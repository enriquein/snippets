SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER PROCEDURE [dbo].[MassAssignControlNumbers] 
DECLARE    @p_cal_yr int, 
    @p_entity_ein char(9),
    @p_pentity_id char(5)  /* Si se envia este parametro, filtramos y solo manejamos ese payable entity */
--AS
--BEGIN

set @p_cal_yr = 2010
set @p_entity_ein = ''
set @p_pentity_id = ''

DECLARE @c_start_number bigint,
        @c_end_number   bigint

-- Iteramos sobre los records de numero de control para formas A
DECLARE entity_cursorA CURSOR FOR
    select start_number, end_number from dbo.ControlNumbers 
    where calendar_year = @p_cal_yr and 
          entity_ein = @p_entity_ein and    
          form_type = 'A'
OPEN entity_cursorA
FETCH NEXT FROM entity_cursorA INTO  @c_start_number, @c_end_number
WHILE @@FETCH_STATUS = 0
BEGIN
    if object_id('tempdb..#ctrlNumsA') is not null drop table #ctrlNumsA

    select @c_start_number + Number as control_number into #ctrlNumsA from Numbers where (@c_start_number + Number) <= @c_end_number order by Number
    
    delete c from #ctrlNumsA c inner join cA_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.pentity_id = isnull(@p_pentity_id, a.pentity_id) and a.entity_ein = @p_entity_ein

    delete c from #ctrlNumsA c inner join A_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.pentity_id = isnull(@p_pentity_id, a.pentity_id) and a.entity_ein = @p_entity_ein

    -- Basicamente lo que hacemos aqui es crear un Id usando el row number. Luego el resultado se 
    -- joinea con la tabla de numeros usando su row number como Id. El resultado final es que cada
    -- record va a tener entonces un numero de control valido para el Update.
    ;WITH CTE AS
    (
        SELECT *, ROW_NUMBER() OVER(ORDER BY pentity_id, social_sec_num_str) Corr
        FROM cA_view 
        where cal_yr = @p_cal_yr and pentity_id = isnull(@p_pentity_id, pentity_id) and entity_ein = @p_entity_ein and
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
          entity_ein = @p_entity_ein and    
          form_type = 'B'
OPEN entity_cursorB
FETCH NEXT FROM entity_cursorB INTO  @c_start_number, @c_end_number
WHILE @@FETCH_STATUS = 0
BEGIN
    if object_id('tempdb..#ctrlNumsB') is not null drop table #ctrlNumsB
    select @c_start_number + Number as control_number into #ctrlNumsB from Numbers where (@c_start_number + Number) <= @c_end_number order by Number
    
    delete c from #ctrlNumsB c inner join cB_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.pentity_id = isnull(@p_pentity_id, a.pentity_id) and a.entity_ein = @p_entity_ein

    delete c from #ctrlNumsB c inner join B_view a on c.control_number = a.control_number_str where a.cal_yr = @p_cal_yr and 
        a.pentity_id = isnull(@p_pentity_id, a.pentity_id) and a.entity_ein = @p_entity_ein

    -- Basicamente lo que hacemos aqui es crear un Id usando el row number. Luego el resultado se 
    -- joinea con la tabla de numeros usando su row number como Id. El resultado final es que cada
    -- record va a tener entonces un numero de control valido para el Update.
    ;WITH CTE AS
    (
        SELECT *, ROW_NUMBER() OVER(ORDER BY pentity_id, social_sec_num_str) Corr
        FROM cB_view 
        where cal_yr = @p_cal_yr and pentity_id = isnull(@p_pentity_id, pentity_id) and entity_ein = @p_entity_ein and
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