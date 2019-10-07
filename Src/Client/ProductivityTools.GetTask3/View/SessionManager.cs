using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public class SessionManager
    {
        string _sesisonKey = "ViewMetadata";
        System.Management.Automation.PSCmdlet _cmdlet;

        public SessionManager(System.Management.Automation.PSCmdlet cmdlet)
        {
            _cmdlet = cmdlet;
        }

        public StructureMetadata ViewMetadata
        {
            get
            {
                var r=_cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                if (r==null)
                {
                    _cmdlet.SessionState.PSVariable.Set(_sesisonKey, new StructureMetadata());
                    r = _cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                }
                return (StructureMetadata)r.Value;
                //var xxxz = Cmdlet.SessionState.PSVariable.Get("fsa");
                //List<string> xxx = new List<string>();
                //xxx.Add("p");
                //xxx.Add("d");
                //Environment.SetEnvironmentVariable("pawel", "Fdsa");
                //Cmdlet.SessionState.PSVariable.Set("fsa", xxx);
            }
            set
            {
                _cmdlet.SessionState.PSVariable.Set(_sesisonKey, value);
            }
        }

 
            
    }
}
