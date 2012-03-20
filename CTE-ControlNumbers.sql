SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Enrique Ramirez
-- Create date: 2/25/2009
-- Description: Un resuelve temporero ya que estamos super contra del reloj y hemos tenido problemas imprimiendo desde PRPay etc.
--              Este procedure asigna numeros de control al payable_entity especificado con sus rangos. NO le va a re-asignar numeros
--              a quienes ya lo tienen, y deberia no generar numeros duplicados.
-- =============================================
-- Author:      Enrique Ramirez
-- Create date: 3/29/2011
-- Description: Modificacion grande para que recoja los datos de la tabla ControlNumbers. Ahora deberia tambien intentar iterar
--              por todas las entidades si no se le especifica un payable_entity como argumento.
--              Tambien se cambio la logica que utilizaba cursores para asignar cada numero de control por un CTE.
--              La diferencia en tiempo fue drastica. Para todo lo que es SSS (incl. REC) bajo de 8 minutos a 4 segundos.
-- =============================================
--ALTER PROCEDURE [dbo].[MassAssignControlNumbers] 
DECLARE    @p_cal_yr int, 
    @p_pay_entity_ein char(9),
    @p_payable_entity_id char(5)  /* Si se envia este parametro, filtramos y solo manejamos ese payable entity */
--AS
--BEGIN

set @p_cal_yr = 2010
set @p_pay_entity_ein = ''
set @p_payable_entity_id = 'REC'

DECLARE @c_start_number bigint,
        @c_end_number   bigint

-- Iteramos sobre los records de numero de control para formas A
DECLARE entity_cursorA CURSOR FOR
    select start_number, end_number from TSMEDPSMT02.SSSclmsPROD.dbo.ControlNumbers 
    where calendar_year = @p_cal_yr and 
          pay_entity_ein = @p_pay_entity_ein and    
          form_type = 'A'
OPEN entity_cursorA
FETCH NEXT FROM entity_cursorA INTO  @c_start_number, @c_end_number
WHILE @@FETCH_STATUS = 0
BEGIN
    if object_id('tempdb..#ctrlNumsA') is not null drop table #ctrlNumsA

    select @c_start_number + Number as control_number into #ctrlNumsA from Numbers where (@c_start_number + Number) <= @c_end_number order by Number
    
    delete c from #ctrlNumsA c inner join candidatos_4806A_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.payable_entity_id = isnull(@p_payable_entity_id, a.payable_entity_id) and a.pay_entity_ein = @p_pay_entity_ein

    delete c from #ctrlNumsA c inner join sss_informativas_4806A_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.payable_entity_id = isnull(@p_payable_entity_id, a.payable_entity_id) and a.pay_entity_ein = @p_pay_entity_ein

    -- Basicamente lo que hacemos aqui es crear un Id usando el row number. Luego el resultado se 
    -- joinea con la tabla de numeros usando su row number como Id. El resultado final es que cada
    -- record va a tener entonces un numero de control valido para el Update.
    ;WITH CTE AS
    (
        SELECT *, ROW_NUMBER() OVER(ORDER BY payable_entity_id, social_sec_num_str) Corr
        FROM candidatos_4806A_view 
        where cal_yr = @p_cal_yr and payable_entity_id = isnull(@p_payable_entity_id, payable_entity_id) and pay_entity_ein = @p_pay_entity_ein and
              control_number_str is null
    )
    UPDATE CTE
    SET CTE.control_number_str = Numbers.control_number,
        CTE.form_type = 'A'
    FROM CTE
    JOIN (  SELECT control_number, ROW_NUMBER() OVER(ORDER BY control_number) Corr
            FROM #ctrlNumsA) Numbers
    ON CTE.Corr = Numbers.Corr

    FETCH NEXT FROM entity_cursorA INTO @c_start_number, @c_end_number
END

CLOSE entity_cursorA
DEALLOCATE entity_cursorA 

-- Iteramos sobre los records de numero de control para formas B
DECLARE entity_cursorB CURSOR FOR
    select start_number, end_number from ControlNumbers 
    where calendar_year = @p_cal_yr and 
          pay_entity_ein = @p_pay_entity_ein and    
          form_type = 'B'
OPEN entity_cursorB
FETCH NEXT FROM entity_cursorB INTO  @c_start_number, @c_end_number
WHILE @@FETCH_STATUS = 0
BEGIN
    if object_id('tempdb..#ctrlNumsB') is not null drop table #ctrlNumsB
    select @c_start_number + Number as control_number into #ctrlNumsB from Numbers where (@c_start_number + Number) <= @c_end_number order by Number
    
    delete c from #ctrlNumsB c inner join candidatos_4806B_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.payable_entity_id = isnull(@p_payable_entity_id, a.payable_entity_id) and a.pay_entity_ein = @p_pay_entity_ein

    delete c from #ctrlNumsB c inner join sss_informativas_4806B_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.payable_entity_id = isnull(@p_payable_entity_id, a.payable_entity_id) and a.pay_entity_ein = @p_pay_entity_ein

    -- Basicamente lo que hacemos aqui es crear un Id usando el row number. Luego el resultado se 
    -- joinea con la tabla de numeros usando su row number como Id. El resultado final es que cada
    -- record va a tener entonces un numero de control valido para el Update.
    ;WITH CTE AS
    (
        SELECT *, ROW_NUMBER() OVER(ORDER BY payable_entity_id, social_sec_num_str) Corr
        FROM candidatos_4806B_view 
        where cal_yr = @p_cal_yr and payable_entity_id = isnull(@p_payable_entity_id, payable_entity_id) and pay_entity_ein = @p_pay_entity_ein and
              control_number_str is null
    )
    UPDATE CTE
    SET CTE.control_number_str = Numbers.control_number,
        CTE.form_type = 'B'
    FROM CTE
    JOIN (  SELECT control_number, ROW_NUMBER() OVER(ORDER BY control_number) Corr
            FROM #ctrlNumsB) Numbers
    ON CTE.Corr = Numbers.Corr

    FETCH NEXT FROM entity_cursorB INTO @c_start_number, @c_end_number
END

CLOSE entity_cursorB
DEALLOCATE entity_cursorB 

if object_id('tempdb..#ctrlNumsA') is not null drop table #ctrlNumsA
if object_id('tempdb..#ctrlNumsB') is not null drop table #ctrlNumsB

END 