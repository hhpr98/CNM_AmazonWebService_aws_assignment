using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_Assignment01
{
    class PositionClass
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public PositionClass(float top,float left,float width,float height)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }
    }
}
