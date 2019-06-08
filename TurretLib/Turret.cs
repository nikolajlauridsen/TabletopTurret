using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

using PrioritySerialCom;

namespace TurretLib
{
    public class Turret
    {
        public int[] TurretPosition { get; }
        public bool IsActive { get; private set; }
        private SerialCom _com;

        public Turret(string port, int baud)
        {
            _com = new SerialCom(port, baud);
            TurretPosition = new int[2];
        }

        public void Move(int x, int y, int moveTime)
        {
            if(x < 0 || x > 180) throw new Exception($"X not between 0 and 180, value: {x}");
            if(y < 0 || y > 180) throw new Exception($"Y not between 0 and 180, value: {y}");
            if(IsActive == false) throw new Exception("Turret not active");
            _com.Write(new PriorityMessage($"m:{x},{y}", 2, moveTime));
        }

        public void Move(int x, int y)
        {
            Move(x, y, 50);
            TurretPosition[0] = x;
            TurretPosition[1] = y;
        }

        public void Fire()
        {
            _com.Write(new PriorityMessage("f", 1, 40));
        }

        public void Activate()
        {
            IsActive = true;
            _com.Start();
        }

        public void Deactivate()
        {
            IsActive = false;
            _com.Stop();
        }

        
    }
}
