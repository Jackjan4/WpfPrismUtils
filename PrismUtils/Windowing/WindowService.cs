using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using De.JanRoslan.WpfPrismUtils.Windowing;
using De.JanRoslan.WpfPrismUtils.Windowing.Exceptions;
using Unity;

namespace De.JanRoslan.WpfPrismUtils.Windowing {
    public class WindowService : IWindowService {



        /// <summary>
        /// The unity container of this application
        /// </summary>
        private readonly IUnityContainer _container;



        /// <summary>
        /// Keeping track of which windows are currently open, so we can list them or close them later
        /// </summary>
        private readonly Dictionary<string, Window> _windows;



        public WindowService(IUnityContainer container) {
            _container = container;
            _windows = new Dictionary<string, Window>();
        }


        /// <summary>
        /// Opens the WPF-Window with the given name
        /// </summary>
        public void OpenWindow(string name) {

            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, types).Show();
        }

        public void OpenWindowDialog(string name) {

            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, types).ShowDialog();
        }

        public void CloseWindow(string name) {
            _windows[name].Close();
            _windows.Remove(name);
        }

        public Window InitWindow(string name, Type[] customTypes = null) {
            Type[] types = customTypes ?? Assembly.GetCallingAssembly().GetTypes();

            try {
                FrameworkElement obj = InitFrameWorkElement(name, types);

                if (!(obj is Window)) {
                    throw new NotAWindowException();
                }

                Window win = (Window) obj;
                _windows[name] = win;
                return win;

            } catch (Exception es) {
                throw es;
            }
        }

        public FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null) {
            var list = new List<Type[]>
            {
                Assembly.GetCallingAssembly().GetTypes(),
                Assembly.GetExecutingAssembly().GetTypes(),
                Assembly.GetCallingAssembly().GetTypes()
            };

            // No custom assembly was delivered
            if (customTypes != null) {
                list.Add(customTypes);
            }

            object obj = null;
            foreach (Type[] types in list) {

                Type first = types.FirstOrDefault(t => t.Name == name);
                if (first == null) {
                    continue;
                }
                obj = _container.Resolve(first);
                if (obj != null) {
                    break;
                }
            }
            if (obj == null) {
                throw new FrameworkElementNotFoundException();
            }

            if (!(obj is FrameworkElement)) {
                throw new NotAFrameworkElementException();
            }


            return (FrameworkElement) obj;
        }
    }
}
