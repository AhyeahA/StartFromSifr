using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Text;

public class PythonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Option1_ExecProcess();
    }



    static void Option1_ExecProcess()
    {
        var psi = new ProcessStartInfo();
        psi.FileName = @"C:\Program Files\Unity\Hub\Editor\2019.4.9f1\Editor\Data\PlaybackEngines\WebGLSupport\BuildTools\Emscripten_Win\python\2.7.5.3_64bit\python.exe";
        var script = @"C:\Users\alhar\Downloads\Score_Calculator\Score_Calculator\main.py";
        var collection = "Level 3 Room 1";
        var document = "1";
        var userSentence = "قاد محمد للذهاب إلى عمله";
        psi.Arguments = $"\"{script}\"\"{collection}\"\"{document}\"\"{userSentence}\"";

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var errors = "";
        var results = "";

        using(var process = Process.Start(psi))
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
        }

        UnityEngine.Debug.Log("Errors: " + errors);
        UnityEngine.Debug.Log("results: " + results.ToString());

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
