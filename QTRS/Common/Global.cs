using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QTRS
{
    class Global
    {
        // Login Info
        public static LoginInfo loginInfo = null;
        // Configs
        public static Common.Configs configInfo = null; 

        // Grid color
        public static Color GRID_COLUMN_FORE_COLOR = Color.FromArgb(116, 133, 134);
        public static Color GRID_COLUMN_BACK_COLOR = Color.FromArgb(247, 248, 252);
        public static Color GRID_ROW_FORE_COLOR = Color.FromArgb(116, 133, 134);
        public static Color GRID_ROW_BACK_COLOR = Color.FromArgb(255, 255, 255);

        public static Color GRID_COLOR = Color.FromArgb(225, 227, 228);

        public const int GRID_COLUMN_HEIGHT = 21;
        public const int GRID_ROW_HEIGHT = 21;

        // DB string separator
        public static string DB_VALUE_SEPARATOR = "^";

        // File type
        public static int FILETYPE_COMPONENT = 0; 
        public static int FILETYPE_PRODUCT_Q = 1; 
        public static int FILETYPE_PRODUCT_M = 2;

        // Master file type
        public enum eMasterFileType {  component, componentTestMethod, product, productTestMethod, importInspection };

        // Report Type
        public enum eReportType { qualityManagement, manufactureManagement, finalQualityManagement, componentDrugTest };


        public static string comNumber = ""; 


    }
}
