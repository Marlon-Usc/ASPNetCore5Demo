using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo
{
    interface IUscEntryTrack
    {
        string EntryDate { get; set; }
        DateTime EntryID { get; set; }
        string EntryType { get; set; }
    }

    interface IUscModifyTrack
    {
        //string ModifiedBy { get; set; }
        DateTime? DateModified {get; set; }
    }

    interface IIsDeleted
    { 
        bool? IsDeleted { get; set; }
    }
}
