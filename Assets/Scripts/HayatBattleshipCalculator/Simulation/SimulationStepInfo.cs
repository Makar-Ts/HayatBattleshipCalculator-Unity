using System;
using UnityEngine;

namespace HayatBattleshipCalculator
{
    public struct SimulationStepInfo
    {
        public float stepTime;

        private float _fadeTime;
        private float _straightTime;


        public SimulationStepInfo(float step = 6, float fade = 0.5f)
        {
            stepTime = step;
            _straightTime = stepTime - fade * 2;
            _fadeTime = fade;
        }


        public readonly float GetFadeInStart()
        {
            return 0;
        }

        public readonly float CalcFadeIn(float lifetime)
        {
            return (-Mathf.Cos(lifetime / _fadeTime * Mathf.PI) + 1) / 2;
        }


        public readonly float GetStraightStart()
        {
            return _fadeTime;
        }

        public readonly float CalcStraight(float lifetime)
        {
            return 1;
        }


        public readonly float GetFadeOutStart()
        {
            return _fadeTime + _straightTime;
        }

        public readonly float CalcFadeOut(float lifetime)
        {
            return (Mathf.Cos((lifetime - _fadeTime - _straightTime) / _fadeTime * Mathf.PI) + 1) / 2;
        }
    }


    public class SimulatedStep
    {
        public float lifetime;
        private readonly SimulationStepInfo info;


        public SimulatedStep(SimulationStepInfo i)
        {
            lifetime = 0f;
            info = i;
        }


        public float Step(float deltaTime)
        {
            lifetime += deltaTime;

            if (lifetime > info.stepTime)
            {
                return 0;
            }
            else if (info.GetFadeOutStart() <= lifetime)
            {
                return info.CalcFadeOut(lifetime);
            }
            else if (info.GetStraightStart() <= lifetime)
            {
                return info.CalcStraight(lifetime);
            }
            else if (info.GetFadeInStart() <= lifetime)
            {
                return info.CalcFadeIn(lifetime);
            }

            return 0;
        }
    }
}