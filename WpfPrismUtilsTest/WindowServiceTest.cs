using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using De.JanRoslan.WpfPrismUtils.Windowing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPrismUtilsTest {
    [TestClass]
    public class WindowServiceTest {





        [TestMethod]
        public void TestInitFrameworkElement()
        {
            IWindowService subject = new WindowService(new DummyUnityContainer());


            subject.InitFrameWorkElement("TestUserControl",Assembly.GetExecutingAssembly().GetTypes());

            
        }



        [TestMethod]
        public void TestInitWindow()
        {
            IWindowService subject = new WindowService(new DummyUnityContainer());

            subject.InitWindow("TestWindow", Assembly.GetExecutingAssembly().GetTypes());
        }



        [TestMethod]
        public void TestInitWindowParameters()
        {
            IWindowService subject = new WindowService(new DummyUnityContainer());

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("test", 5);
            parameters.Add("lol", "aloha");


            subject.InitWindow("TestWindow", parameters, Assembly.GetExecutingAssembly().GetTypes());

            Dictionary<string, object> result = subject.GetWindowContext("TestWindow");

            Debug.Assert(parameters.Count == result.Count);
            Debug.Assert((int)result["test"] == 5);
            Debug.Assert(result["lol"].Equals("aloha"));
        }



    }
}
