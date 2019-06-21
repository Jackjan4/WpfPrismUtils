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

        FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null);
    }
}
