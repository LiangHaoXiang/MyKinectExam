/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.CaptureStatusChangedEventArgs
    //
    public sealed partial class CaptureStatusChangedEventArgs : RootSystem.EventArgs, Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal CaptureStatusChangedEventArgs(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_AddRefObject(ref _pNative);
        }

        ~CaptureStatusChangedEventArgs()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<CaptureStatusChangedEventArgs>(_pNative);
                Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceModelBuilderCaptureStatus Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_get_PreviousCaptureStatus(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceModelBuilderCaptureStatus PreviousCaptureStatus
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("CaptureStatusChangedEventArgs");
                }

                return Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_get_PreviousCaptureStatus(_pNative);
            }
        }

        private void __EventCleanup()
        {
        }
    }

}
