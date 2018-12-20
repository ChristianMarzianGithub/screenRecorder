using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenRecorderLib;

namespace DotNEtScreenRecorder
{
    class recorder
    {
        Recorder _rec;
        Stream _outStream;
        public void CreateRecording()
        {
            string videoPath = Path.Combine(Path.GetTempPath(), "test.mp4");
            _rec = Recorder.CreateRecorder();
            _rec.OnRecordingComplete += Rec_OnRecordingComplete;
            _rec.OnRecordingFailed += Rec_OnRecordingFailed;
            _rec.OnStatusChanged += Rec_OnStatusChanged;
            //Record to a file
            //string videoPath = Path.Combine(Path.GetTempPath(), "test.mp4");
            _rec.Record(videoPath);
            //..Or to a stream
            //_outStream = new MemoryStream();
            //_rec.Record(_outStream);
        }
        void EndRecording()
        {
            _rec.Stop();
        }
        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            //Get the file path if recorded to a file
            string path = e.FilePath;
            //or do something with your stream
            //... something ...
            _outStream?.Dispose();
        }
        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
            _outStream?.Dispose();
        }
        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
        }
    }
}
