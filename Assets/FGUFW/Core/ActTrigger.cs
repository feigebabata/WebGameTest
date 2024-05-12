// using System;
// using System.Collections.Generic;

// namespace FGUFW
// {

//     [Serializable]
//     public class ActTrigger<KEY>
//     {
//         /// <summary>
//         /// 触发条件
//         /// </summary>
//         public List<IRequire> Requires = new List<IRequire>();

//         /// <summary>
//         /// 目标
//         /// </summary>
//         public int Target;

//         /// <summary>
//         /// 行为
//         /// </summary>
//         public List<int> Acts = new List<int>();
        

//         public bool MatchRequire(KEY k)
//         {
//             int length = Requires.Count;
//             for (int i = 0; i < length; i++)
//             {
//                 if(!Requires[i].Match(k))
//                 {
//                     return false;
//                 }
//             }
//             return true;
//         }

//         /// <summary>
//         /// 触发条件接口
//         /// </summary>
//         public interface IRequire
//         {
//             bool Match(in KEY key);
//         }

//         public interface IOnTriggerAct
//         {
//             void OnTriggerAct(float worldTime);
//         }
//     }


// }

/*
行为触发器:
    作用层
    层等级
    响应事件
        -无
        -自动
        -自动_预热
        -接触
        -普攻键
        -技能键
        -普攻
        -被攻击
        -死亡
        -装载
        -卸载
        -移动控制
    使用次数
    使用时间
    Cd
    概率
    自定义规则判定
    过热次数
    过热时间
    过热Cd
    目标
        -自己位置
        -接触目标
        -自己范围内
        -目标范围内
        -自己
        -反弹对象(被攻击)
    行为
        添加buff
        移除buff
        造成伤害
        释放技能
        触发事件
        播放动画
        播放音效
        播放特效
        玩家移动
        击退
        向玩家移动
        直线突进
        射击子弹
    状态
        -无
        -出现
        -消失
        -待击发
        -击发cd中
        -击发中
        -冷却中
*/
