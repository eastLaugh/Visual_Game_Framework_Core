using AutumnFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace VGF.SL
{
    [Bean]
    public class ItemDisplayData
    { 

        private HashSet<string> mSavedKeys = new HashSet<string>();
        public bool HasSavedKey(string key)
        {
            return mSavedKeys.Contains(key);
        }

        public void AddSavedKey(string key)
        {
            mSavedKeys.Add(key);
        }

        public string Save()
        {
            return string.Join(";@;",mSavedKeys);
        }

        public void load(string data)
        {
            
            if (data == string.Empty)
            {
                mSavedKeys = new HashSet<string>();
            }
            else
            {
                mSavedKeys = data.Split(";@;").ToHashSet();
            }
        }

        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys.Clear();
        }
    }

}
