#pragma warning disable 108
// ReSharper disable RedundantUsingDirective
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable UnusedType.Local
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantCast
// ReSharper disable IdentifierTypo
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantUnsafeContext
// ReSharper disable RedundantBaseQualifier
// ReSharper disable EmptyStatement
// ReSharper disable RedundantAttributeParentheses
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MicroCom.Runtime;

namespace AvaloniaWebView.Interop
{
    internal unsafe partial interface IDispatch : global::MicroCom.Runtime.IUnknown
    {
        void GetTypeInfoCount(uint* pctinfo);
        void GetTypeInfo(uint iTInfo, int lcid, void** ppTInfo);
        void GetIDsOfNames(System.Guid* riid, System.Char** rgszNames, uint cNames, int lcid, int* rgDispId);
        void Invoke(int dispIdMember, System.Guid* riid, int lcid, short wFlags, void* pDispParams, void* pVarResult, void* pExcepInfo, uint* puArgErr);
    }

    internal unsafe partial interface IWebBrowser : IDispatch
    {
        void GoBack();
        void GoForward();
        void GoHome();
        void GoSearch();
        void Navigate(IntPtr URL, void* Flags, void* TargetFrameName, void* PostData, void* Headers);
        void Refresh();
        void Refresh2(void* Level);
        void Stop();
        IDispatch Application();
        IDispatch Parent();
        IDispatch Container();
        IDispatch Document();
        int TopLevelContainer();
        IntPtr Type();
        long Left();
        void SetLeft(long Left);
        long Top();
        void SetTop(long Top);
        long Width();
        void SetWidth(long Width);
        long Height();
        void SetHeight(long Height);
        IntPtr LocationName();
        IntPtr LocationURL();
        int Busy();
    }

    internal unsafe partial interface IWebBrowserApp : IWebBrowser
    {
        void Quit();
        int ClientToWindow(int* pcx);
        void PutProperty(IntPtr Property, void* vtValue);
        void* GetProperty(IntPtr Property);
        IntPtr Name();
        IntPtr HWND();
        IntPtr FullName();
        IntPtr Path();
        int Visible();
        void SetVisible(int Value);
        int StatusBar();
        void SetStatusBar(int Value);
        IntPtr StatusText();
        void SetStatusText(IntPtr StatusText);
        int ToolBar();
        void SetToolBar(int Value);
        int MenuBar();
        void SetMenuBar(int Value);
        int FullScreen();
        void SetFullScreen(int bFullScreen);
    }

    internal unsafe partial interface IWebBrowser2 : IWebBrowserApp
    {
        void Navigate2(void** URL, void** Flags, void** TargetFrameName, void** PostData, void** Headers);
    }
}

namespace AvaloniaWebView.Interop.Impl
{
    internal unsafe partial class __MicroComIDispatchProxy : global::MicroCom.Runtime.MicroComProxyBase, IDispatch
    {
        public void GetTypeInfoCount(uint* pctinfo)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 0])(PPV, pctinfo);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GetTypeInfoCount failed", __result);
        }

        public void GetTypeInfo(uint iTInfo, int lcid, void** ppTInfo)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, uint, int, void*, int>)(*PPV)[base.VTableSize + 1])(PPV, iTInfo, lcid, ppTInfo);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GetTypeInfo failed", __result);
        }

        public void GetIDsOfNames(System.Guid* riid, System.Char** rgszNames, uint cNames, int lcid, int* rgDispId)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, void*, uint, int, void*, int>)(*PPV)[base.VTableSize + 2])(PPV, riid, rgszNames, cNames, lcid, rgDispId);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GetIDsOfNames failed", __result);
        }

        public void Invoke(int dispIdMember, System.Guid* riid, int lcid, short wFlags, void* pDispParams, void* pVarResult, void* pExcepInfo, uint* puArgErr)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, void*, int, short, void*, void*, void*, void*, int>)(*PPV)[base.VTableSize + 3])(PPV, dispIdMember, riid, lcid, wFlags, pDispParams, pVarResult, pExcepInfo, puArgErr);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Invoke failed", __result);
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit()
        {
            global::MicroCom.Runtime.MicroComRuntime.Register(typeof(IDispatch), new Guid("00020400-0000-0000-C000-000000000046"), (p, owns) => new __MicroComIDispatchProxy(p, owns));
        }

        protected __MicroComIDispatchProxy(IntPtr nativePointer, bool ownsHandle) : base(nativePointer, ownsHandle)
        {
        }

        protected override int VTableSize => base.VTableSize + 4;
    }

    unsafe class __MicroComIDispatchVTable : global::MicroCom.Runtime.MicroComVtblBase
    {
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GetTypeInfoCountDelegate(void* @this, uint* pctinfo);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GetTypeInfoCount(void* @this, uint* pctinfo)
        {
            IDispatch __target = null;
            try
            {
                {
                    __target = (IDispatch)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GetTypeInfoCount(pctinfo);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GetTypeInfoDelegate(void* @this, uint iTInfo, int lcid, void** ppTInfo);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GetTypeInfo(void* @this, uint iTInfo, int lcid, void** ppTInfo)
        {
            IDispatch __target = null;
            try
            {
                {
                    __target = (IDispatch)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GetTypeInfo(iTInfo, lcid, ppTInfo);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GetIDsOfNamesDelegate(void* @this, System.Guid* riid, System.Char** rgszNames, uint cNames, int lcid, int* rgDispId);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GetIDsOfNames(void* @this, System.Guid* riid, System.Char** rgszNames, uint cNames, int lcid, int* rgDispId)
        {
            IDispatch __target = null;
            try
            {
                {
                    __target = (IDispatch)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GetIDsOfNames(riid, rgszNames, cNames, lcid, rgDispId);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int InvokeDelegate(void* @this, int dispIdMember, System.Guid* riid, int lcid, short wFlags, void* pDispParams, void* pVarResult, void* pExcepInfo, uint* puArgErr);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Invoke(void* @this, int dispIdMember, System.Guid* riid, int lcid, short wFlags, void* pDispParams, void* pVarResult, void* pExcepInfo, uint* puArgErr)
        {
            IDispatch __target = null;
            try
            {
                {
                    __target = (IDispatch)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Invoke(dispIdMember, riid, lcid, wFlags, pDispParams, pVarResult, pExcepInfo, puArgErr);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        protected __MicroComIDispatchVTable()
        {
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, uint*, int>)&GetTypeInfoCount); 
#else
            base.AddMethod((GetTypeInfoCountDelegate)GetTypeInfoCount); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, uint, int, void**, int>)&GetTypeInfo); 
#else
            base.AddMethod((GetTypeInfoDelegate)GetTypeInfo); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, System.Guid*, System.Char**, uint, int, int*, int>)&GetIDsOfNames); 
#else
            base.AddMethod((GetIDsOfNamesDelegate)GetIDsOfNames); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, System.Guid*, int, short, void*, void*, void*, uint*, int>)&Invoke); 
#else
            base.AddMethod((InvokeDelegate)Invoke); 
#endif
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit() => global::MicroCom.Runtime.MicroComRuntime.RegisterVTable(typeof(IDispatch), new __MicroComIDispatchVTable().CreateVTable());
    }

    internal unsafe partial class __MicroComIWebBrowserProxy : __MicroComIDispatchProxy, IWebBrowser
    {
        public void GoBack()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 0])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GoBack failed", __result);
        }

        public void GoForward()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 1])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GoForward failed", __result);
        }

        public void GoHome()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 2])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GoHome failed", __result);
        }

        public void GoSearch()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 3])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GoSearch failed", __result);
        }

        public void Navigate(IntPtr URL, void* Flags, void* TargetFrameName, void* PostData, void* Headers)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, IntPtr, void*, void*, void*, void*, int>)(*PPV)[base.VTableSize + 4])(PPV, URL, Flags, TargetFrameName, PostData, Headers);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Navigate failed", __result);
        }

        public void Refresh()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 5])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Refresh failed", __result);
        }

        public void Refresh2(void* Level)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 6])(PPV, Level);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Refresh2 failed", __result);
        }

        public void Stop()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 7])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Stop failed", __result);
        }

        public IDispatch Application()
        {
            int __result;
            void* __marshal_ppDisp = null;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 8])(PPV, &__marshal_ppDisp);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Application failed", __result);
            return global::MicroCom.Runtime.MicroComRuntime.CreateProxyOrNullFor<IDispatch>(__marshal_ppDisp, true);
        }

        public IDispatch Parent()
        {
            int __result;
            void* __marshal_ppDisp = null;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 9])(PPV, &__marshal_ppDisp);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Parent failed", __result);
            return global::MicroCom.Runtime.MicroComRuntime.CreateProxyOrNullFor<IDispatch>(__marshal_ppDisp, true);
        }

        public IDispatch Container()
        {
            int __result;
            void* __marshal_ppDisp = null;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 10])(PPV, &__marshal_ppDisp);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Container failed", __result);
            return global::MicroCom.Runtime.MicroComRuntime.CreateProxyOrNullFor<IDispatch>(__marshal_ppDisp, true);
        }

        public IDispatch Document()
        {
            int __result;
            void* __marshal_ppDisp = null;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 11])(PPV, &__marshal_ppDisp);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Document failed", __result);
            return global::MicroCom.Runtime.MicroComRuntime.CreateProxyOrNullFor<IDispatch>(__marshal_ppDisp, true);
        }

        public int TopLevelContainer()
        {
            int __result;
            int pBool = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 12])(PPV, &pBool);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("TopLevelContainer failed", __result);
            return pBool;
        }

        public IntPtr Type()
        {
            int __result;
            IntPtr Type = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 13])(PPV, &Type);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Type failed", __result);
            return Type;
        }

        public long Left()
        {
            int __result;
            long pl = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 14])(PPV, &pl);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Left failed", __result);
            return pl;
        }

        public void SetLeft(long Left)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, long, int>)(*PPV)[base.VTableSize + 15])(PPV, Left);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetLeft failed", __result);
        }

        public long Top()
        {
            int __result;
            long pl = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 16])(PPV, &pl);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Top failed", __result);
            return pl;
        }

        public void SetTop(long Top)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, long, int>)(*PPV)[base.VTableSize + 17])(PPV, Top);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetTop failed", __result);
        }

        public long Width()
        {
            int __result;
            long pl = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 18])(PPV, &pl);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Width failed", __result);
            return pl;
        }

        public void SetWidth(long Width)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, long, int>)(*PPV)[base.VTableSize + 19])(PPV, Width);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetWidth failed", __result);
        }

        public long Height()
        {
            int __result;
            long pl = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 20])(PPV, &pl);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Height failed", __result);
            return pl;
        }

        public void SetHeight(long Height)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, long, int>)(*PPV)[base.VTableSize + 21])(PPV, Height);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetHeight failed", __result);
        }

        public IntPtr LocationName()
        {
            int __result;
            IntPtr LocationName = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 22])(PPV, &LocationName);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("LocationName failed", __result);
            return LocationName;
        }

        public IntPtr LocationURL()
        {
            int __result;
            IntPtr LocationURL = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 23])(PPV, &LocationURL);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("LocationURL failed", __result);
            return LocationURL;
        }

        public int Busy()
        {
            int __result;
            int pBool = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 24])(PPV, &pBool);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Busy failed", __result);
            return pBool;
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit()
        {
            global::MicroCom.Runtime.MicroComRuntime.Register(typeof(IWebBrowser), new Guid("EAB22AC1-30C1-11CF-A7EB-0000C05BAE0B"), (p, owns) => new __MicroComIWebBrowserProxy(p, owns));
        }

        protected __MicroComIWebBrowserProxy(IntPtr nativePointer, bool ownsHandle) : base(nativePointer, ownsHandle)
        {
        }

        protected override int VTableSize => base.VTableSize + 25;
    }

    unsafe class __MicroComIWebBrowserVTable : __MicroComIDispatchVTable
    {
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GoBackDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GoBack(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GoBack();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GoForwardDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GoForward(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GoForward();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GoHomeDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GoHome(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GoHome();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GoSearchDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GoSearch(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.GoSearch();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int NavigateDelegate(void* @this, IntPtr URL, void* Flags, void* TargetFrameName, void* PostData, void* Headers);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Navigate(void* @this, IntPtr URL, void* Flags, void* TargetFrameName, void* PostData, void* Headers)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Navigate(URL, Flags, TargetFrameName, PostData, Headers);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int RefreshDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Refresh(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Refresh();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int Refresh2Delegate(void* @this, void* Level);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Refresh2(void* @this, void* Level)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Refresh2(Level);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int StopDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Stop(void* @this)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Stop();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int ApplicationDelegate(void* @this, void** ppDisp);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Application(void* @this, void** ppDisp)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Application();
                        *ppDisp = global::MicroCom.Runtime.MicroComRuntime.GetNativePointer(__result, true);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int ParentDelegate(void* @this, void** ppDisp);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Parent(void* @this, void** ppDisp)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Parent();
                        *ppDisp = global::MicroCom.Runtime.MicroComRuntime.GetNativePointer(__result, true);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int ContainerDelegate(void* @this, void** ppDisp);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Container(void* @this, void** ppDisp)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Container();
                        *ppDisp = global::MicroCom.Runtime.MicroComRuntime.GetNativePointer(__result, true);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int DocumentDelegate(void* @this, void** ppDisp);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Document(void* @this, void** ppDisp)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Document();
                        *ppDisp = global::MicroCom.Runtime.MicroComRuntime.GetNativePointer(__result, true);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int TopLevelContainerDelegate(void* @this, int* pBool);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int TopLevelContainer(void* @this, int* pBool)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.TopLevelContainer();
                        *pBool = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int TypeDelegate(void* @this, IntPtr* Type);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Type(void* @this, IntPtr* Type)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Type();
                        *Type = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int LeftDelegate(void* @this, long* pl);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Left(void* @this, long* pl)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Left();
                        *pl = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetLeftDelegate(void* @this, long Left);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetLeft(void* @this, long Left)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetLeft(Left);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int TopDelegate(void* @this, long* pl);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Top(void* @this, long* pl)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Top();
                        *pl = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetTopDelegate(void* @this, long Top);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetTop(void* @this, long Top)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetTop(Top);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int WidthDelegate(void* @this, long* pl);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Width(void* @this, long* pl)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Width();
                        *pl = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetWidthDelegate(void* @this, long Width);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetWidth(void* @this, long Width)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetWidth(Width);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int HeightDelegate(void* @this, long* pl);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Height(void* @this, long* pl)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Height();
                        *pl = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetHeightDelegate(void* @this, long Height);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetHeight(void* @this, long Height)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetHeight(Height);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int LocationNameDelegate(void* @this, IntPtr* LocationName);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int LocationName(void* @this, IntPtr* LocationName)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.LocationName();
                        *LocationName = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int LocationURLDelegate(void* @this, IntPtr* LocationURL);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int LocationURL(void* @this, IntPtr* LocationURL)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.LocationURL();
                        *LocationURL = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int BusyDelegate(void* @this, int* pBool);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Busy(void* @this, int* pBool)
        {
            IWebBrowser __target = null;
            try
            {
                {
                    __target = (IWebBrowser)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Busy();
                        *pBool = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        protected __MicroComIWebBrowserVTable()
        {
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&GoBack); 
#else
            base.AddMethod((GoBackDelegate)GoBack); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&GoForward); 
#else
            base.AddMethod((GoForwardDelegate)GoForward); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&GoHome); 
#else
            base.AddMethod((GoHomeDelegate)GoHome); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&GoSearch); 
#else
            base.AddMethod((GoSearchDelegate)GoSearch); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr, void*, void*, void*, void*, int>)&Navigate); 
#else
            base.AddMethod((NavigateDelegate)Navigate); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&Refresh); 
#else
            base.AddMethod((RefreshDelegate)Refresh); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void*, int>)&Refresh2); 
#else
            base.AddMethod((Refresh2Delegate)Refresh2); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&Stop); 
#else
            base.AddMethod((StopDelegate)Stop); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void**, int>)&Application); 
#else
            base.AddMethod((ApplicationDelegate)Application); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void**, int>)&Parent); 
#else
            base.AddMethod((ParentDelegate)Parent); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void**, int>)&Container); 
#else
            base.AddMethod((ContainerDelegate)Container); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void**, int>)&Document); 
#else
            base.AddMethod((DocumentDelegate)Document); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&TopLevelContainer); 
#else
            base.AddMethod((TopLevelContainerDelegate)TopLevelContainer); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&Type); 
#else
            base.AddMethod((TypeDelegate)Type); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long*, int>)&Left); 
#else
            base.AddMethod((LeftDelegate)Left); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long, int>)&SetLeft); 
#else
            base.AddMethod((SetLeftDelegate)SetLeft); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long*, int>)&Top); 
#else
            base.AddMethod((TopDelegate)Top); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long, int>)&SetTop); 
#else
            base.AddMethod((SetTopDelegate)SetTop); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long*, int>)&Width); 
#else
            base.AddMethod((WidthDelegate)Width); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long, int>)&SetWidth); 
#else
            base.AddMethod((SetWidthDelegate)SetWidth); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long*, int>)&Height); 
#else
            base.AddMethod((HeightDelegate)Height); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, long, int>)&SetHeight); 
#else
            base.AddMethod((SetHeightDelegate)SetHeight); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&LocationName); 
#else
            base.AddMethod((LocationNameDelegate)LocationName); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&LocationURL); 
#else
            base.AddMethod((LocationURLDelegate)LocationURL); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&Busy); 
#else
            base.AddMethod((BusyDelegate)Busy); 
#endif
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit() => global::MicroCom.Runtime.MicroComRuntime.RegisterVTable(typeof(IWebBrowser), new __MicroComIWebBrowserVTable().CreateVTable());
    }

    internal unsafe partial class __MicroComIWebBrowserAppProxy : __MicroComIWebBrowserProxy, IWebBrowserApp
    {
        public void Quit()
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int>)(*PPV)[base.VTableSize + 0])(PPV);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Quit failed", __result);
        }

        public int ClientToWindow(int* pcx)
        {
            int __result;
            int pcy = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, void*, int>)(*PPV)[base.VTableSize + 1])(PPV, pcx, &pcy);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("ClientToWindow failed", __result);
            return pcy;
        }

        public void PutProperty(IntPtr Property, void* vtValue)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, IntPtr, void*, int>)(*PPV)[base.VTableSize + 2])(PPV, Property, vtValue);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("PutProperty failed", __result);
        }

        public void* GetProperty(IntPtr Property)
        {
            int __result;
            void* pvtValue = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, IntPtr, void*, int>)(*PPV)[base.VTableSize + 3])(PPV, Property, &pvtValue);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("GetProperty failed", __result);
            return pvtValue;
        }

        public IntPtr Name()
        {
            int __result;
            IntPtr Name = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 4])(PPV, &Name);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Name failed", __result);
            return Name;
        }

        public IntPtr HWND()
        {
            int __result;
            IntPtr pHWND = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 5])(PPV, &pHWND);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("HWND failed", __result);
            return pHWND;
        }

        public IntPtr FullName()
        {
            int __result;
            IntPtr FullName = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 6])(PPV, &FullName);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("FullName failed", __result);
            return FullName;
        }

        public IntPtr Path()
        {
            int __result;
            IntPtr Path = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 7])(PPV, &Path);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Path failed", __result);
            return Path;
        }

        public int Visible()
        {
            int __result;
            int pBool = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 8])(PPV, &pBool);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Visible failed", __result);
            return pBool;
        }

        public void SetVisible(int Value)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, int>)(*PPV)[base.VTableSize + 9])(PPV, Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetVisible failed", __result);
        }

        public int StatusBar()
        {
            int __result;
            int pBool = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 10])(PPV, &pBool);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("StatusBar failed", __result);
            return pBool;
        }

        public void SetStatusBar(int Value)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, int>)(*PPV)[base.VTableSize + 11])(PPV, Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetStatusBar failed", __result);
        }

        public IntPtr StatusText()
        {
            int __result;
            IntPtr StatusText = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 12])(PPV, &StatusText);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("StatusText failed", __result);
            return StatusText;
        }

        public void SetStatusText(IntPtr StatusText)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, IntPtr, int>)(*PPV)[base.VTableSize + 13])(PPV, StatusText);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetStatusText failed", __result);
        }

        public int ToolBar()
        {
            int __result;
            int Value = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 14])(PPV, &Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("ToolBar failed", __result);
            return Value;
        }

        public void SetToolBar(int Value)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, int>)(*PPV)[base.VTableSize + 15])(PPV, Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetToolBar failed", __result);
        }

        public int MenuBar()
        {
            int __result;
            int Value = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 16])(PPV, &Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("MenuBar failed", __result);
            return Value;
        }

        public void SetMenuBar(int Value)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, int>)(*PPV)[base.VTableSize + 17])(PPV, Value);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetMenuBar failed", __result);
        }

        public int FullScreen()
        {
            int __result;
            int pbFullScreen = default;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*PPV)[base.VTableSize + 18])(PPV, &pbFullScreen);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("FullScreen failed", __result);
            return pbFullScreen;
        }

        public void SetFullScreen(int bFullScreen)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, int, int>)(*PPV)[base.VTableSize + 19])(PPV, bFullScreen);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("SetFullScreen failed", __result);
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit()
        {
            global::MicroCom.Runtime.MicroComRuntime.Register(typeof(IWebBrowserApp), new Guid("0002DF05-0000-0000-C000-000000000046"), (p, owns) => new __MicroComIWebBrowserAppProxy(p, owns));
        }

        protected __MicroComIWebBrowserAppProxy(IntPtr nativePointer, bool ownsHandle) : base(nativePointer, ownsHandle)
        {
        }

        protected override int VTableSize => base.VTableSize + 20;
    }

    unsafe class __MicroComIWebBrowserAppVTable : __MicroComIWebBrowserVTable
    {
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int QuitDelegate(void* @this);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Quit(void* @this)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Quit();
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int ClientToWindowDelegate(void* @this, int* pcx, int* pcy);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int ClientToWindow(void* @this, int* pcx, int* pcy)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.ClientToWindow(pcx);
                        *pcy = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int PutPropertyDelegate(void* @this, IntPtr Property, void* vtValue);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int PutProperty(void* @this, IntPtr Property, void* vtValue)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.PutProperty(Property, vtValue);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int GetPropertyDelegate(void* @this, IntPtr Property, void** pvtValue);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int GetProperty(void* @this, IntPtr Property, void** pvtValue)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.GetProperty(Property);
                        *pvtValue = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int NameDelegate(void* @this, IntPtr* Name);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Name(void* @this, IntPtr* Name)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Name();
                        *Name = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int HWNDDelegate(void* @this, IntPtr* pHWND);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int HWND(void* @this, IntPtr* pHWND)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.HWND();
                        *pHWND = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int FullNameDelegate(void* @this, IntPtr* FullName);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int FullName(void* @this, IntPtr* FullName)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.FullName();
                        *FullName = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int PathDelegate(void* @this, IntPtr* Path);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Path(void* @this, IntPtr* Path)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Path();
                        *Path = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int VisibleDelegate(void* @this, int* pBool);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Visible(void* @this, int* pBool)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.Visible();
                        *pBool = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetVisibleDelegate(void* @this, int Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetVisible(void* @this, int Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetVisible(Value);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int StatusBarDelegate(void* @this, int* pBool);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int StatusBar(void* @this, int* pBool)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.StatusBar();
                        *pBool = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetStatusBarDelegate(void* @this, int Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetStatusBar(void* @this, int Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetStatusBar(Value);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int StatusTextDelegate(void* @this, IntPtr* StatusText);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int StatusText(void* @this, IntPtr* StatusText)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.StatusText();
                        *StatusText = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetStatusTextDelegate(void* @this, IntPtr StatusText);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetStatusText(void* @this, IntPtr StatusText)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetStatusText(StatusText);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int ToolBarDelegate(void* @this, int* Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int ToolBar(void* @this, int* Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.ToolBar();
                        *Value = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetToolBarDelegate(void* @this, int Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetToolBar(void* @this, int Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetToolBar(Value);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int MenuBarDelegate(void* @this, int* Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int MenuBar(void* @this, int* Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.MenuBar();
                        *Value = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetMenuBarDelegate(void* @this, int Value);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetMenuBar(void* @this, int Value)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetMenuBar(Value);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int FullScreenDelegate(void* @this, int* pbFullScreen);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int FullScreen(void* @this, int* pbFullScreen)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    {
                        var __result = __target.FullScreen();
                        *pbFullScreen = __result;
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int SetFullScreenDelegate(void* @this, int bFullScreen);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int SetFullScreen(void* @this, int bFullScreen)
        {
            IWebBrowserApp __target = null;
            try
            {
                {
                    __target = (IWebBrowserApp)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.SetFullScreen(bFullScreen);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        protected __MicroComIWebBrowserAppVTable()
        {
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int>)&Quit); 
#else
            base.AddMethod((QuitDelegate)Quit); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int*, int>)&ClientToWindow); 
#else
            base.AddMethod((ClientToWindowDelegate)ClientToWindow); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr, void*, int>)&PutProperty); 
#else
            base.AddMethod((PutPropertyDelegate)PutProperty); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr, void**, int>)&GetProperty); 
#else
            base.AddMethod((GetPropertyDelegate)GetProperty); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&Name); 
#else
            base.AddMethod((NameDelegate)Name); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&HWND); 
#else
            base.AddMethod((HWNDDelegate)HWND); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&FullName); 
#else
            base.AddMethod((FullNameDelegate)FullName); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&Path); 
#else
            base.AddMethod((PathDelegate)Path); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&Visible); 
#else
            base.AddMethod((VisibleDelegate)Visible); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, int>)&SetVisible); 
#else
            base.AddMethod((SetVisibleDelegate)SetVisible); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&StatusBar); 
#else
            base.AddMethod((StatusBarDelegate)StatusBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, int>)&SetStatusBar); 
#else
            base.AddMethod((SetStatusBarDelegate)SetStatusBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)&StatusText); 
#else
            base.AddMethod((StatusTextDelegate)StatusText); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, IntPtr, int>)&SetStatusText); 
#else
            base.AddMethod((SetStatusTextDelegate)SetStatusText); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&ToolBar); 
#else
            base.AddMethod((ToolBarDelegate)ToolBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, int>)&SetToolBar); 
#else
            base.AddMethod((SetToolBarDelegate)SetToolBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&MenuBar); 
#else
            base.AddMethod((MenuBarDelegate)MenuBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, int>)&SetMenuBar); 
#else
            base.AddMethod((SetMenuBarDelegate)SetMenuBar); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int*, int>)&FullScreen); 
#else
            base.AddMethod((FullScreenDelegate)FullScreen); 
#endif
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, int, int>)&SetFullScreen); 
#else
            base.AddMethod((SetFullScreenDelegate)SetFullScreen); 
#endif
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit() => global::MicroCom.Runtime.MicroComRuntime.RegisterVTable(typeof(IWebBrowserApp), new __MicroComIWebBrowserAppVTable().CreateVTable());
    }

    internal unsafe partial class __MicroComIWebBrowser2Proxy : __MicroComIWebBrowserAppProxy, IWebBrowser2
    {
        public void Navigate2(void** URL, void** Flags, void** TargetFrameName, void** PostData, void** Headers)
        {
            int __result;
            __result = (int)((delegate* unmanaged[Stdcall]<void*, void*, void*, void*, void*, void*, int>)(*PPV)[base.VTableSize + 0])(PPV, URL, Flags, TargetFrameName, PostData, Headers);
            if (__result != 0)
                throw new System.Runtime.InteropServices.COMException("Navigate2 failed", __result);
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit()
        {
            global::MicroCom.Runtime.MicroComRuntime.Register(typeof(IWebBrowser2), new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E"), (p, owns) => new __MicroComIWebBrowser2Proxy(p, owns));
        }

        protected __MicroComIWebBrowser2Proxy(IntPtr nativePointer, bool ownsHandle) : base(nativePointer, ownsHandle)
        {
        }

        protected override int VTableSize => base.VTableSize + 1;
    }

    unsafe class __MicroComIWebBrowser2VTable : __MicroComIWebBrowserAppVTable
    {
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
        delegate int Navigate2Delegate(void* @this, void** URL, void** Flags, void** TargetFrameName, void** PostData, void** Headers);
#if NET5_0_OR_GREATER
        [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })] 
#endif
        static int Navigate2(void* @this, void** URL, void** Flags, void** TargetFrameName, void** PostData, void** Headers)
        {
            IWebBrowser2 __target = null;
            try
            {
                {
                    __target = (IWebBrowser2)global::MicroCom.Runtime.MicroComRuntime.GetObjectFromCcw(new IntPtr(@this));
                    __target.Navigate2(URL, Flags, TargetFrameName, PostData, Headers);
                }
            }
            catch (System.Runtime.InteropServices.COMException __com_exception__)
            {
                return __com_exception__.ErrorCode;
            }
            catch (System.Exception __exception__)
            {
                global::MicroCom.Runtime.MicroComRuntime.UnhandledException(__target, __exception__);
                return unchecked((int)0x80004005u);
            }

            return 0;
        }

        protected __MicroComIWebBrowser2VTable()
        {
#if NET5_0_OR_GREATER
            base.AddMethod((delegate* unmanaged[Stdcall]<void*, void**, void**, void**, void**, void**, int>)&Navigate2); 
#else
            base.AddMethod((Navigate2Delegate)Navigate2); 
#endif
        }

        [System.Runtime.CompilerServices.ModuleInitializer()]
        internal static void __MicroComModuleInit() => global::MicroCom.Runtime.MicroComRuntime.RegisterVTable(typeof(IWebBrowser2), new __MicroComIWebBrowser2VTable().CreateVTable());
    }
}