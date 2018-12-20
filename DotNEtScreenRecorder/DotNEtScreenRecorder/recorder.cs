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
            RecorderOptions options = new RecorderOptions
            {
                RecorderMode = RecorderMode.Video,
                //If throttling is disabled, out of memory exceptions may eventually crash the program,
                //depending on encoder settings and system specifications.
                IsThrottlingDisabled = false,
                //Hardware encoding is enabled by default.
                IsHardwareEncodingEnabled = true,
                //Low latency mode provides faster encoding, but can reduce quality.
                IsLowLatencyEnabled = false,
                //Fast start writes the mp4 header at the beginning of the file, to facilitate streaming.
                IsMp4FastStartEnabled = false,
                AudioOptions = new AudioOptions
                {
                    Bitrate = AudioBitrate.bitrate_128kbps,
                    Channels = AudioChannels.Stereo,
                    IsAudioEnabled = true
                },
                VideoOptions = new VideoOptions
                {
                    //BitrateMode = BitrateControlMode.UnconstrainedVBR,
                    Bitrate = 8000 * 1000,
                    Framerate = 60,
                    IsMousePointerEnabled = true,
                    IsFixedFramerate = true,
                    EncoderProfile = H264Profile.Main
                }
            };


            string videoPath = Path.Combine(Path.GetTempPath(), "test.mp4");
            _rec = Recorder.CreateRecorder(options);
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
