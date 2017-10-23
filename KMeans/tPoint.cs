using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class tPoint
    {
        public List<byte> Vector { get; set; }
        public tPoint Label { get; set; }
        public static int id = 0;
        public int ID { get; set; }
        static Random rd = new System.Random();

        public float KhoangCach(tPoint b)
        {
            float result = 0;
            for (int i = 0; i < Vector.Count; i++)
            {
                result += (float)Math.Pow(Vector[i] - b.Vector[i], 2);
            }
            return (float)Math.Sqrt(result);
        }

        public tPoint(int count = 900)
        {
            Vector = new List<byte>();
            id++;
            ID = id;
            
            for (int i = 0; i < count; i++)
            {
                Vector.Add((byte)(rd.Next()));
            }
            Label = null;
        }

        public tPoint(List<int> lst, int count = 900)
        {
            id++;
            ID = id;
            Label = null;
            Vector = new List<byte>();
            for (int i = 0; i < count; i++)
            {
                Vector.Add(i < lst.Count ? (byte)lst[i] : (byte)0);
            }
        }

        public bool SelectCenter(List<tPoint> lstCenter)
        {
            if (lstCenter == null || lstCenter.Count == 0)
            {
                Label = null;
                return true;
            }
            int indexSelect = 0;
            float min = KhoangCach(lstCenter[indexSelect]);
            for (int i = 1; i < lstCenter.Count; i++)
            {
                float save = KhoangCach(lstCenter[i]);
                if (min > save)
                {
                    min = save;
                    indexSelect = i;
                }
            }
            if (Label == null || lstCenter[indexSelect].ID != Label.ID)
            {
                Label = lstCenter[indexSelect];
                return true;
            }
            return false;
        }

        public void ZeroVector()
        {
            for (int i = 0; i < Vector.Count; i++)
            {
                Vector[i] = (byte)0;
            }
        }

        public static tPoint operator +(tPoint a, tPoint b)
        {
            for (int i = 0; i < a.Vector.Count; i++)
            {
                a.Vector[i] += b.Vector[i];
            }
            return a;
        }

        public void UpdateCenter(List<tPoint> lst)
        {
            if (lst == null || lst.Count == 0) return;
            tPoint save = new tPoint();
            save.ZeroVector();
            foreach (var item in lst)
            {
                save += item;
            }
            for (int i = 0; i < save.Vector.Count; i++)
            {
                save.Vector[i] = (byte)(save.Vector[i] / lst.Count);
            }
            this.Vector = save.Vector;
        }

    }
}
