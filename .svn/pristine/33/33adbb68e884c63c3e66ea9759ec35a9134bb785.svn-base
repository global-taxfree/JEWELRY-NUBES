using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace JEWELRY_NUBES.Util
{
    class UserPrintDocument : PrintDocument
    {
        //public delegate void UserPrintPageEventHandler(object sender, PrintPageEventArgs e, object docid, object retailer, object tourist, object goods, object adsinfo);
        public delegate void UserPrintPageEventHandler(object sender, PrintPageEventArgs e);
        public event UserPrintPageEventHandler UserPrintPageEvent;

        public UserPrintDocument()
        {
            // Added by AsCarion [2015.01.26]
            // Print Page Size Set.
            // Paper Size : 80mm X 297 mm (302px X 1122px) 에서 길이만 3배로 늘린다.
            //this.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Receipt", 284, 1110);
            //this.DefaultPageSettings.Landscape = true;
            this.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4 Portrate", 790, 1110);
        }

        protected override void OnPrintPage(PrintPageEventArgs args)
        {
            // raise my version of PrintPageEventHandler with added m_context            
            this.UserPrintPageEvent(this, args);
        }
    }
}
