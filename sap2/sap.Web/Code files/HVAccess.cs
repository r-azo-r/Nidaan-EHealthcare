using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;


namespace sap.Web
{
    public abstract class HVAccess:HealthServicePage
    {
        static Dictionary<Guid, HealthRecordInfo> l;
        //List 

        public HVAccess()
        {
            l = PersonInfo.AuthorizedRecords;

        }
    }
}