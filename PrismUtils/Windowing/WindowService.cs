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
        private readonly Dictionary<string, Tuple<Window, Dictionary<string, object>>> _windows;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public WindowService(IUnityContainer container) {
            _container = container;
            _windows = new Dictionary<string, Tuple<Window, Dictionary<string, object>>>();
        }


        /// <summary>
        /// Opens the WPF-Window with the given name
        /// </summary>
        public void OpenWindow(string name) {
            OpenWindow(name, null);
        }

        public void OpenWindowDialog(string name) {
            OpenWindowDialog(name, null);
        }

        public void OpenWindow(string name, Dictionary<string, object> parameters) {
            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, parameters, types).Show();
        }

        public void OpenWindowDialog(string name, Dictionary<string, object> parameters) {
            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, parameters, types).ShowDialog();
        }

        public void CloseWindow(string name) {
            _windows[name].Item1.Close();
            _windows.Remove(name);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="customTypes"></param>
        /// <returns></returns>
        public Window InitWindow(string name, Type[] customTypes = null) {
            return InitWindow(name, null, customTypes);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <param name="customTypes"></param>
        /// <returns></returns>
        public Window InitWindow(string name, Dictionary<string, object> parameters, Type[] customTypes = null) {
            Type[] types = customTypes ?? Assembly.GetCallingAssembly().GetTypes();

            try {
                FrameworkElement obj = InitFrameWorkElement(name, types);

                if (!(obj is Window)) {
                    throw new NotAWindowException();
                }

                Window win = (Window) obj;
                _windows[name] = new Tuple<Window, Dictionary<string, object>>(win, parameters ?? new Dictionary<string, object>());
                return win;

            } catch (Exception es) {
                throw es;
            }
        }

        public Dictionary<string, object> GetWindowContext(string name) {
            return _windows[name].Item2;
        }

        public FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null) {
            var list = new HashSet<Type[]>
            {
                Assembly.GetCallingAssembly().GetTypes(),
                Assembly.GetExecutingAssembly().GetTypes(),
                Assembly.GetEntryAssembly()?.GetTypes()
            };

            // No custom assembly was delivered
            if (customTypes != null) {
                list.Add(customTypes);
            }

            object obj = null;
            foreach (Type[] types in list) {

                // List is null (for example when an added assembly is null
                if (types == null) {
                    continue;
                }

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
