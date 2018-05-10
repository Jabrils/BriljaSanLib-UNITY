// This library is called the Brilja-san Library, completely written by Jabril Ashe.
// The whole objective of this library is to help speed up workflows by creating frequently used functions.
// This is version 0.1.1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BriljaSanLib
{
    // This class is designed to Get things
    public class Get
    {
        /// <summary>
        /// This loads a texture from any path, make sure to add the extentions like .png, & I haven't tested this outside of PNGs
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Texture2D LoadTXT(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }

        /// <summary>
        /// This dramatically shortens the Unity Engine GetComponent Function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Find"></param>
        /// <returns></returns>
        public static T QuickComponent<T>(string Find) where T : class
        {
            return GameObject.Find(Find).GetComponent<T>();
        }

        /// <summary>
        /// This shoots a ray from 100 whatever above down to give you a random Vector3 location that will be colliding with the nearest ground collider
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="zMin"></param>
        /// <param name="zMax"></param>
        /// <returns></returns>
        public static Vector3 RandomMapPosition(float xMin, float xMax, float zMin, float zMax)
        {
            Vector3 starting = new Vector3(Random.Range(xMin / 2, xMax / 2), 100, Random.Range(zMin / 2, zMax / 2));
            Ray groundRay = new Ray(starting, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(groundRay, out hit))
            {
                return hit.point;
            }
            else
            {
                return Vector3.zero;
            }
            //Debug.DrawLine(starting, hit.point, Color.red,5f);
        }

        // 
        public static Vector3 RandomSqRadPos(Vector3 origin, float sqRad)
        {
            Vector3 starting = new Vector3(Random.Range(origin.x - sqRad, origin.x + sqRad), 100, Random.Range(origin.z - sqRad, origin.z + sqRad));
            Ray groundRay = new Ray(starting, Vector3.down);
            RaycastHit hit;

            // 
            if (Physics.Raycast(groundRay, out hit))
            {
                return hit.point;
            }
            else
            {
                return origin;
            }
        }

        /// <summary>
        /// This capitalises the first letter in a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

    // This class is designed to Generate things
    public class Generate
    {
        /// <summary>
        /// This will return a time stamp to your string input
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string AddTimeStamp(string n)
        {
            string comb = n + "_" + System.DateTime.Now;
            comb = comb.Replace('/', '-');
            comb = comb.Replace(':', '-');
            comb = comb.Replace(' ', '_');
            return comb;
        }

        /// <summary>
        /// This generates a random name, there are currently 2 type, type 0 is the most random, type 1 is more user input
        /// </summary>
        /// <param name="type"></param>
        /// <param name="charas"></param>
        /// <returns></returns>
        public static string Name(int type, int charas)
        {
            // This creates a randomly generated name length
            if (charas == 0)
            {
                charas = Random.Range(3, 8);
            }

            string theName = "";

            // The first type is randomly generated, the second type is a different approach
            if (type == 0)
            {
                string[] vowels = new string[]
                {
                "a","e","i","o","u"
                };
                string[] constonants = new string[]
                {
                "b","c","d","f","g","h","j","k","l","m","n","p","q","r","s","t","v","x","z","w","y"
                };

                int con = Random.Range(0, 2);
                int vow = Random.Range(0, 2);
                for (int i = 0; i < charas; i++)
                {
                    if (con < vow)
                    {
                        theName += constonants[Random.Range(0, constonants.Length)];
                        con++;
                    }
                    else
                    {
                        theName += vowels[Random.Range(0, vowels.Length)];// + constonants[Random.Range(0, constonants.Length)] + vowels[Random.Range(0, vowels.Length)] + constonants[Random.Range(0, constonants.Length)] + vowels[Random.Range(0, vowels.Length)];
                        vow++;
                        //
                    }
                }
            }
            else if (type == 1)
            {
                string[] prefix = new string[]
                {
                "Ra","Dra","Fu", "Se","Tin", "Fik", "Broth", "Ruf"
                };
                string[] root = new string[]
                    {
                "mini","kyte","lyn","ferk","sert","tryne", ""
                    };
                string[] suffix = new string[]
                    {
                "ly", "mon", "dile", "ang","son", "a", ""
                    };
                theName = prefix[Random.Range(0, prefix.Length)] + root[Random.Range(0, root.Length)] + suffix[Random.Range(0, suffix.Length)];
            }

            return Get.Capitalize(theName);
        }

        /// <summary>
        /// Returns a random float
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandomFloat(float min, float max)
        {
            float newF = Random.Range(min, max);

            return newF;
        }

        /// <summary>
        /// Returns a random Int
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            int newI = Random.Range(min, max);

            return newI;
        }

        /// <summary>
        /// Use this function to save a photo with a capture name
        /// </summary>
        /// <param name="capName"></param>
        public static void ScreenCap(string capName)
        {
            string saveFolder = Path.Combine(Application.dataPath, "Screencaps");

            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            //
            ScreenCapture.CaptureScreenshot(saveFolder + $"/{capName}_{AddTimeStamp("")}.png");
        }

        /// <summary>
        /// Use this function to save a photo with a capture name, & a location
        /// </summary>
        /// <param name="capName"></param>
        public static void ScreenCap(string capName, string location)
        {
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            //
            ScreenCapture.CaptureScreenshot(location + $"/{capName}_{AddTimeStamp("")}.png");
        }
    }

    public class Enable
    {
        /// <summary>
        /// Exits the game
        /// </summary>
        /// <param name="key"></param>
        public static void ExitGame(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                Application.Quit();
            }
        }
    }

    public class Make
    {
        public static void Rotate(Transform trans, float roSpeed, float axisX, float axisY, float axisZ)
        {
            trans.Rotate(new Vector3(roSpeed * axisX, roSpeed * axisY, roSpeed * axisZ));
        }
    }

    public class Alg
    {
        /// <summary>
        /// This will return a bubble sorted array of ints
        /// </summary>
        /// <param name="low2High"></param>
        /// <param name="probs"></param>
        /// <returns></returns>
        public static int[] BubbleSort(bool low2High, int[] probs)
        {
            int checker = 1;
            while (checker != 0)
            {
                checker = 0;

                if (low2High)
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] > probs[i])
                        {
                            int temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] < probs[i])
                        {
                            int temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
            }

            return probs;
        }

        /// <summary>
        /// This will return a bubble sorted array of doubles
        /// </summary>
        /// <param name="low2High"></param>
        /// <param name="probs"></param>
        /// <returns></returns>
        public static double[] BubbleSort(bool low2High, double[] probs)
        {
            int checker = 1;
            while (checker != 0)
            {
                checker = 0;

                if (low2High)
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] > probs[i])
                        {
                            double temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] < probs[i])
                        {
                            double temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
            }

            return probs;
        }

        /// <summary>
        /// This will return a bubble sorted array of floats
        /// </summary>
        /// <param name="low2High"></param>
        /// <param name="probs"></param>
        /// <returns></returns>
        public static float[] BubbleSort(bool low2High, float[] probs)
        {
            int checker = 1;
            while (checker != 0)
            {
                checker = 0;

                if (low2High)
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] > probs[i])
                        {
                            float temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < probs.Length; i++)
                    {
                        if (probs[i - 1] < probs[i])
                        {
                            float temp = probs[i - 1];
                            probs[i - 1] = probs[i];
                            probs[i] = temp;
                            checker++;
                        }
                    }
                }
            }

            return probs;
        }
    }
}