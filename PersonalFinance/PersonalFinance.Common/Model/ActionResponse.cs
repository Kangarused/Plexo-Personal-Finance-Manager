using System.Collections.Generic;
using System.Linq;
using T4TS;


namespace PersonalFinance.Common.Model
{
    
    public class ActionResponse
    {
        public bool Succeed { get; set; }
        public List<string> Errors { get; set; }

        public static ActionResponse CreateWithErrors(IEnumerable<string> errors)
        {
            return new ActionResponse
            {
                Errors = errors.ToList(),
                Succeed = false
            };
        }
    }
   
    public class ActionResponseGeneric<T> : ActionResponse
    {
      
        public T Response { get; set; }

        public new static ActionResponseGeneric<T> CreateWithErrors(IEnumerable<string> errors)
        {
            return new ActionResponseGeneric<T>
            {
                Errors = errors.ToList(),
                Succeed = false
            };
        }
    }
}
