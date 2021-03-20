using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace AbiCALC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int navMin = 50, navMax = 150;
        
        private bool navPanelMovementFinished = true;
        private bool navPanelMovementAim = true;

        private object navPanelMovementFinishedLock = new object();
        private object navPanelMovementAimLock = new object();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void tgl_button_clicked(object sender, RoutedEventArgs e)
        {
            lock (navPanelMovementAimLock) 
            {               
                navPanelMovementAim = !(bool)(sender as ToggleButton).IsChecked;                  
            }
            lock (navPanelMovementFinishedLock)
            {
                if(navPanelMovementFinished) 
                {
                    navPanelMovementFinished = false;
                    nav_pnl_expand(30, new TimeSpan(0, 0, 2));                  
                }             
            }
            
        }

        private async void nav_pnl_expand(int fps, TimeSpan duration)
        {
            float pos = 0f;
            int frame;
            int frames = 0;           
            int maxFrame = (int)(((float)duration.TotalSeconds * fps));
            lock (navPanelMovementAimLock) 
            {
                frame = !navPanelMovementAim ? 0 : maxFrame;
            }
            DateTime time = DateTime.Now;
            while (true) 
            {
                
                lock (navPanelMovementAimLock)
                {
                    //calc next frame
                    bool dir = !navPanelMovementAim;
                    frame += dir ? 1 : -1;

                    //increment frames count
                    frames++;

                    //get pos [0;1]
                    pos = (float)frame / (float)maxFrame;

                    //apply pos
                    nav_panel.Width = (int)(navMin + map(pos,f) * (navMax - navMin));

                    //break?
                    if ((frame == (((bool)dir) ? maxFrame : 0)))
                    {
                        lock (navPanelMovementFinishedLock)
                        {
                            navPanelMovementFinished = true;
                            break;
                        }
                    }
                }

                //wait
                int timeNeeded = (int)(DateTime.Now.Subtract(time).TotalMilliseconds);

                int timeExpected = 1000 / fps * frames;

                int timeDelta = timeExpected - timeNeeded;
                await Task.Delay(Math.Max(timeDelta,0));

            }          
        }

        private float f(float input) 
        {
            return (float)(Math.Exp(input));
        }

        private float map(float f, Func<float, float> func) 
        {
            float deltaY = -func(0f);
            float scaleY = 1 / (deltaY + func(1f));
            return scaleY * (deltaY + func(f));
        }
    }
}
