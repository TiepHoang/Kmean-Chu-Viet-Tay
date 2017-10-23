using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Core
    {
        public List<tPoint> lstPoint { get; set; }
        public List<tPoint> lstCenter { get; set; }
        public int K { get; set; }

        public Core(List<tPoint> lstPoint,int K=9)
        {
            this.lstPoint = lstPoint;
            this.K = K;

            lstCenter = new List<tPoint>();
            for (int i = 0; i < K; i++)
            {
                lstCenter.Add(new tPoint());
            }
        }

        public void Run()
        {
            bool _isContinue = true;

            while (_isContinue)
            {
                foreach (var item in lstPoint)
                {
                    _isContinue =  item.SelectCenter(lstCenter) && _isContinue;
                }
                for (int i=0; i<lstCenter.Count; i++)
                {
                    lstCenter[i].UpdateCenter(lstPoint.Where(q => q.Label.ID == lstCenter[i].ID).ToList());
                }
            }
        }
    }
}
