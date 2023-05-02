using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    struct UniformGrid
    {
        public double coordinate_begin { set; get; }
        public double coordinate_end { set; get; }
        public int note_number { get; set; }
        public UniformGrid(double left_point, double right_point, int note_number)
        {
            this.note_number = note_number;
            coordinate_begin = left_point;
            coordinate_end = right_point;
        }
        public double Step
        {
            get
            {
                return (coordinate_end - coordinate_begin) / note_number;
            }
        }
        public override string ToString()
        {
            return $"coordinate_begin = {coordinate_begin}\ncoordinate_end = {coordinate_end}\nstep = {Step}\nnote_number = {note_number}\n";
        }
        public string ToLongString(string format)
        {
            return $"coordinate_begin = {coordinate_begin.ToString(format)}\ncoordinate_end = {coordinate_end.ToString(format)}\nstep = {Step.ToString(format)}\nnote_number = {note_number}\n";
        }
    }
}
