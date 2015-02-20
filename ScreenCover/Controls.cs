using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ScreenCover
{
    public class CustomTab : Label
    {
        public CustomTab()
        {
            this.AutoSize = false;
            this.Font = new Font("微软雅黑", 10f, this.Font.Style | FontStyle.Bold);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }
        public CustomTab(string text)
            : this()
        {
            this.Text = text;
            ResizeControl();
        }

        public void ResizeControl()
        {
            Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");
            int count = 0;
            foreach (char c in this.Text)
            {
                if (regex.IsMatch(c.ToString()))
                {
                    count++;
                }
            }

            this.Size = new Size((10 + Convert.ToInt32((Math.Round(((this.Text.Length - count) * 7.5) + (count * 15)))) + 10), 30);
        }
    }

    public class PinBtn : PictureBox
    {
        Image Pin_Normal;
        Image Pin_Press;
        Image Pin_Hover;
        Image Unpin_Normal;
        Image Unpin_Press;
        Image Unpin_Hover;
        public bool isPin = false;

        public PinBtn()
        {
            this.Size = new Size(30, 30);
            this.MouseEnter += PinBtn_MouseEnter;
            this.MouseLeave += PinBtn_MouseLeave;
            this.MouseDown += PinBtn_MouseDown;
        }

        void PinBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPin)
            {
                this.Image = Pin_Press;
            }
            else
            {
                this.Image = Unpin_Press;
            }
        }

        void PinBtn_MouseLeave(object sender, EventArgs e)
        {
            if (isPin)
            {
                this.Image = Pin_Normal;
            }
            else
            {
                this.Image = Unpin_Normal;
            }
        }

        void PinBtn_MouseEnter(object sender, EventArgs e)
        {
            if (isPin)
            {
                this.Image = Pin_Hover;
            }
            else
            {
                this.Image = Unpin_Hover;
            }
        }

        public PinBtn(Image pin_normal, Image pin_press, Image pin_hover, Image unpin_normal, Image unpin_press, Image unpin_hover, bool pinstate)
            : this()
        {
            this.Pin_Normal = pin_normal;
            this.Pin_Press = pin_press;
            this.Pin_Hover = pin_hover;
            this.Unpin_Normal = unpin_normal;
            this.Unpin_Press = unpin_press;
            this.Unpin_Hover = unpin_hover;
            SwapState(pinstate);
        }

        public void SwapState(bool state)
        {
            isPin = state;
            if (isPin)
            {
                this.Image = this.Pin_Normal;
            }
            else
            {
                this.Image = this.Unpin_Normal;
            }
        }
    }

    public class CustomBtn : PictureBox
    {
        Image Normal;
        Image Press;
        Image Hover;

        public CustomBtn()
        {
            this.Size = new Size(30, 30);
            this.MouseEnter += CustomBtn_MouseEnter;
            this.MouseLeave += CustomBtn_MouseLeave;
            this.MouseUp += CustomBtn_MouseUp;
            this.MouseDown += CustomBtn_MouseDown;
        }

        void CustomBtn_MouseDown(object sender, MouseEventArgs e)
        {
            this.Image = Press;
        }

        void CustomBtn_MouseUp(object sender, MouseEventArgs e)
        {
            this.Image = Normal;
        }

        void CustomBtn_MouseLeave(object sender, EventArgs e)
        {
            this.Image = Normal;
        }

        void CustomBtn_MouseEnter(object sender, EventArgs e)
        {
            this.Image = Hover;
        }

        public CustomBtn(Image normal, Image press, Image hover)
            : this()
        {
            this.Normal = normal;
            this.Press = press;
            this.Hover = hover;

            this.Image = this.Normal;
        }
    }
}
