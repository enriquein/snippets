using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snippets
{
    /// <summary>
    /// Class to facilitate data binding when using a Presenter for MVP.
    /// </summary>
    public class DataSourceContainer
    {
        /// <summary>
        /// Holds the name of the field to pass as "DataValue"
        /// to the control.
        /// </summary>
        public string DataValueFieldName { get; set; }

        /// <summary>
        /// Holds the name of the field to pass as "DataText"
        /// to the control.
        /// </summary>
        public string DataTextFieldName { get; set; }

        /// <summary>
        /// Object containing the actual data. Make sure it has public properties
        /// or fields that match the values in DataValieFieldName and DataTextFieldName.
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// Creates a DataSourceContainer object. 
        /// </summary>
        /// <param name="dataTextFieldName">Name of the property/field that contains the text to be displayed.</param>
        /// <param name="dataValueFieldName">Name of the property/field that has the data related to the current item.</param>
        /// <param name="dataSource">Object containing the actual data. It must have accessible properties of fields matching
        /// the names specified in dataTextFieldName and dataValueFieldName.</param>
        public DataSourceContainer(string dataTextFieldName, string dataValueFieldName, object dataSource)
        {
            this.DataSource = dataSource;
            this.DataTextFieldName = dataTextFieldName;
            this.DataValueFieldName = dataValueFieldName;
        }
    }
}
