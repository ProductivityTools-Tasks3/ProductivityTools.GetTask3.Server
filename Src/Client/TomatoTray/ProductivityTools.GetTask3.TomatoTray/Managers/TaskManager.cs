//using GetTaskContract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

//namespace TomatoesTray.Managers
//{
//    class TaskManager
//    {
//        public void FinishTomato()
//        {

//            ChannelFactory<IGetTask> factory = new ChannelFactory<IGetTask>("GetTask");
//            IGetTask proxy1 = factory.CreateChannel();
//            using (proxy1 as IDisposable)
//            {
//                proxy1.FinishTomato();
//            }
//        }
//    }
//}
