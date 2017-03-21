using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.SystemManagement.RegistryManager{
    public sealed class BasicSecurity {
        private readonly Random CurrentLevel;
        private readonly BasicSecurity NextLevel;

        private readonly static Int32 MIN_CHAR = 33, MAX_CHAR = 126;
        private readonly List<char[]> _Cache = new List<char[]>();

        private Int32 KEY_LENGTH = -0xFFFFF;
        private Int32 FailedAttempts = 0;
        private Int32 PickedInCache = 0;

        public BasicSecurity(bool newLevel, int maxLevels){
            if (maxLevels > 5) maxLevels = 5;

            CurrentLevel = new Random();

            for (int i = 0; i < 10; ++i)
                CurrentLevel.Next();

            Thread.Sleep(50);
            if (newLevel && maxLevels > 0) NextLevel = new BasicSecurity(newLevel, maxLevels-1);
            if (maxLevels <= 0) maxLevels = 1;

            if (KEY_LENGTH == -0xFFFFF) KEY_LENGTH = maxLevels;

        }

        public String GenerateSecurityCode(){
            String final = "";

            if (PickInCache()) SelectChars(out final);
            else
            {
                for (int i = 0; i < KEY_LENGTH; ++i)
                {
                    final += (char)CurrentLevel.Next(MIN_CHAR, MAX_CHAR);
                    if (NextLevel != null)
                        final += NextLevel.GenerateSecurityCode();
                }
                _Cache.Add(final.ToCharArray());
            }

            return final;

        }

        internal void IncreaseDifficulty(){
            if ((FailedAttempts++)%2 == 0)
                if (KEY_LENGTH != -0xFFFFF)
                    KEY_LENGTH++;

            if (FailedAttempts % 4 == 0)
                NextLevel.IncreaseDifficulty();
        }

        private bool PickInCache(){
            if (PickedInCache > 5){
                PickedInCache = 0;
                _Cache.Clear();
                return false;
            }
            PickedInCache++;
            return true;
        }

        private void SelectChars(out String output) {
            String _Shadow = "";
            int lastIndex = 0;
            for (int i = 0; i < KEY_LENGTH; ++i) {
                _Shadow += _Cache.ElementAt(lastIndex = CurrentLevel.Next(0,_Cache.Count)).ElementAt(CurrentLevel.Next(0, _Cache.ElementAt(lastIndex).Count()));
                if (NextLevel != null) SelectChars(out _Shadow);
            }
            output = _Shadow;
        }
    }
}
