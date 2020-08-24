using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JEWELRY_NUBES.Util
{
    class ControlManager
    {
        [Flags]
        public enum enumSizeChange //사이즈 변경 bit 체크
        {
            None = 0,
            b_X_Move = 1 << 0,//횡이동
            b_Y_Move = 1 << 1,//종이동
            b_X_Expend = 1 << 2,//횡늘림
            b_Y_Expend = 1 << 3,//종늘림
            All = int.MaxValue
        };

        private Control m_parent;
        private Size m_parentOriSize;

        Dictionary<Control, enumSizeChange> ctls = new Dictionary<Control, enumSizeChange>();
        Dictionary<Control, Point> ctlsPoint = new Dictionary<Control, Point>();
        Dictionary<Control, Size> ctlsSize = new Dictionary<Control, Size>();

        public ControlManager(Control parentControl)
        {
            if (parentControl is Control)
            {
                m_parent = parentControl;
                m_parentOriSize = new Size(parentControl.Size.Width, parentControl.Size.Height);
            }
        }

        public void ChageLabel(Control curCtl)
        {
            //하위컨트롤 있는 경우는 패스
            if (curCtl.Controls.Count > 1)
            {
                foreach (Control ctl in curCtl.Controls)
                {
                    ChageLabel(ctl);//재귀호출
                }
            }
            else
            {
                //LABEL 만 TEXT 변경. 필요시엔 타 컨트롤도 추가.
                //is(curCtl is Label) 로 비교하는 방법도 있으나 불필요한 컴포넌트는 검색하지 않기 위해 name.index 로 찾는다.
                if (curCtl.Name.IndexOf("LBL") >= 0)
                {
                    string strTempVal = Constants.CONF_MANAGER.getCustomValue("ScreenText"
                        , Constants.SYSTEM_LANGUAGE + "/" + m_parent.Name + "/" + curCtl.Name);
                    if (strTempVal != null && !string.Empty.Equals(strTempVal))
                    {
                        curCtl.Text = strTempVal;
                    }
                }
            }
        }

        public void addControlMove(Control cur_Con, Boolean b_X_Move, Boolean b_Y_Move, Boolean b_X_Expend, Boolean b_Y_Expend)
        {
            enumSizeChange enumSize = enumSizeChange.None;
            if (b_X_Move)
                enumSize |= enumSizeChange.b_X_Move;
            if (b_Y_Move)
                enumSize |= enumSizeChange.b_Y_Move;
            if (b_X_Expend)
                enumSize |= enumSizeChange.b_X_Expend;
            if (b_Y_Expend)
                enumSize |= enumSizeChange.b_Y_Expend;
            ctls.Add(cur_Con, enumSize);//컨트롤별 변경속성 저장
            ctlsPoint.Add(cur_Con, ((Control)cur_Con).Location);//초기 위치 저장
            ctlsSize.Add(cur_Con, ((Control)cur_Con).Size);     //초기 크기 저장
        }

        public void MoveControls()
        {
            foreach (Control de in ctls.Keys)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", de, ctls[de]);
                enumSizeChange tmpEnum = ctls[de];
                Point tempPoint = ctlsPoint[de];
                Size tempSize = ctlsSize[de];

                Size temp_Panel_Size = m_parent.Size;

                //횡이동
                if ((ctls[de] & enumSizeChange.b_X_Move) == enumSizeChange.b_X_Move)
                {
                    tempPoint.X += (temp_Panel_Size.Width - m_parentOriSize.Width);
                }
                //종이동
                if ((ctls[de] & enumSizeChange.b_Y_Move) == enumSizeChange.b_Y_Move)
                {
                    tempPoint.Y += (temp_Panel_Size.Height - m_parentOriSize.Height);
                }
                //횡늘림
                if ((ctls[de] & enumSizeChange.b_X_Expend) == enumSizeChange.b_X_Expend)
                {
                    tempSize.Width += (temp_Panel_Size.Width - m_parentOriSize.Width);
                }
                //종늘림
                if ((ctls[de] & enumSizeChange.b_Y_Expend) == enumSizeChange.b_Y_Expend)
                {
                    tempSize.Height += (temp_Panel_Size.Height - m_parentOriSize.Height);
                }
                ((Control)de).Location = tempPoint;
                ((Control)de).Size = tempSize;
            }

        }
    }
}
