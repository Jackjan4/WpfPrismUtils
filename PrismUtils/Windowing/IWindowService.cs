using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace De.JanRoslan.WpfPrismUtils.Windowing {
    public interface IWindowService
    {

        void OpenWindow(string name);

        void OpenWindowDialog(string name);

        void CloseWindow(string name);

        Window InitWindow(string name, Type[] customTypes = null);

        Window InitWindow(string name, Dictionary<string, object> parameters, Type[] customTypes = null);


        // TODO: Maybe change the string to something like type or directly get the type with Reflection. If not, document the string use here properly
        Dictionary<string, object> GetWindowContext(string name);

        FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null);
    }
}
