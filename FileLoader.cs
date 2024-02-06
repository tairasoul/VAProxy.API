using UnityEngine;
using UnityEngine.Networking;

namespace VAP_API
{
    public class FileLoader
    {
        /// <summary>
        /// Load an audio clip from a file path.
        /// </summary>
        /// <param name="path"></param> File path excluding file:// which is automatically appended.
        public static AudioClip LoadAudioClip(string path) 
        {
            UnityWebRequest request = UnityWebRequest.Get("file://" + path);
            while (!request.isDone) { }
            AudioClip download = DownloadHandlerAudioClip.GetContent(request);
            return download;
        }

        /// <summary>
        /// Load a Texture2D from a file path.
        /// </summary>
        /// <param name="path"></param> File path excluding file:// which is automatically appended.

        public static Texture2D LoadTexture(string path)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://" + path);
            while (!request.isDone) { }
            Texture2D image = DownloadHandlerTexture.GetContent(request);
            return image;
        }


        /// <summary>
        /// Load text from a file path.
        /// </summary>
        /// <param name="path"></param> File path excluding file:// which is automatically appended.

        public static string LoadTextFile(string path)
        {
            string text = new DownloadHandlerFile("file://" + path).text;
            return text;
        }
    }
}
