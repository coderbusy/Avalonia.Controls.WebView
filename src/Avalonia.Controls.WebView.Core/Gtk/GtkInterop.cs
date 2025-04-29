using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace Avalonia.Controls.Gtk;

internal static unsafe partial class GtkInterop
{
    private const string LibGObject = "libgobject-2.0.so.0";
    private const string LibWebKit = "libwebkit2gtk-4.1.so.0";
    private const string LibGLib = "libglib-2.0.so.0";
    private const string LibGio = "libgio-2.0.so.0";
    private const string LibGtk = "libgtk-3.so.0";
    private const string LibGdk = "libgdk-3.so.0";


#if NET7_0_OR_GREATER
    [LibraryImport(LibGLib, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial uint g_log_set_handler(string? logDomain, uint logLevels, IntPtr callback, IntPtr userData);
#else
    [DllImport(LibGLib)]
    internal static extern uint g_log_set_handler(string? logDomain, uint logLevels, IntPtr callback, IntPtr userData);
#endif

    [DllImport(LibWebKit)]
    internal static extern IntPtr webkit_web_view_get_uri(IntPtr webView);

    [DllImport(LibGio)]
    internal static extern void g_free(IntPtr ptr);

    [DllImport(LibGio)]
    internal static extern IntPtr g_application_get_default();

    [DllImport(LibWebKit)]
    internal static extern IntPtr webkit_web_view_new();

#if NET7_0_OR_GREATER
    [LibraryImport(LibWebKit, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void webkit_web_view_load_uri(IntPtr webView, string uri);

    [LibraryImport(LibWebKit, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void webkit_web_view_load_html(IntPtr webView, string content, string? baseUri);

    [LibraryImport(LibWebKit, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void webkit_web_view_run_javascript(IntPtr webView, string script, IntPtr cancellable, IntPtr callback, IntPtr userData);
#else
    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_load_uri(IntPtr webView, string uri);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_load_html(IntPtr webView, string content, string? baseUri);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_run_javascript(IntPtr webView, string script, IntPtr cancellable, IntPtr callback, IntPtr userData);
#endif

    [DllImport(LibWebKit)]
    internal static extern bool webkit_web_view_can_go_back(IntPtr webView);

    [DllImport(LibWebKit)]
    internal static extern bool webkit_web_view_can_go_forward(IntPtr webView);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_go_back(IntPtr webView);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_go_forward(IntPtr webView);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_reload(IntPtr webView);

    [DllImport(LibWebKit)]
    internal static extern void webkit_web_view_stop_loading(IntPtr webView);

    [DllImport(LibWebKit)]
    public static extern nint webkit_web_view_run_javascript_finish(IntPtr webView, IntPtr result, GError** error);

    [DllImport(LibWebKit)]
    public static extern void webkit_javascript_result_unref(IntPtr jsResult);

    [DllImport(LibWebKit)]
    public static extern nint webkit_javascript_result_get_js_value(IntPtr jsResult);

    [DllImport(LibWebKit)]
    public static extern nint jsc_value_to_string(IntPtr value);

    [DllImport(LibGtk)]
    internal static extern IntPtr gtk_scrolled_window_new(IntPtr hadjustment, IntPtr vadjustment);

    [DllImport(LibGtk)]
    internal static extern void gtk_container_add(IntPtr container, IntPtr widget);

    [DllImport(LibGtk)]
    internal static extern void gtk_application_add_window(IntPtr app, IntPtr window);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_show_all(IntPtr widget);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_hide(IntPtr widget);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_destroy(IntPtr widget);

    [DllImport(LibGObject)]
    internal static extern ulong g_error_free(GError* error);

    [DllImport(LibGObject)]
    internal static extern IntPtr g_object_ref(IntPtr handle);

    [DllImport (LibGObject)]
    internal static extern void g_object_unref(IntPtr handle);

    [DllImport(LibGtk)]
    internal static extern IntPtr gtk_window_new(int type);

    [DllImport(LibGtk)]
    internal static extern IntPtr gtk_offscreen_window_new();

    [DllImport(LibGtk)]
    internal static extern void gtk_window_resize(IntPtr window, int width, int height);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_set_size_request(IntPtr widget, int width, int height);

    [DllImport(LibGtk)]
    internal static extern void gtk_window_move(IntPtr window, int x, int y);

    [DllImport(LibGtk)]
    internal static extern void gtk_window_set_resizable(IntPtr window, bool value);

    [DllImport(LibGtk)]
    internal static extern bool gtk_window_get_resizable(IntPtr window);

    [DllImport(LibGtk)]
    internal static extern void gtk_window_set_position(IntPtr window, int positionType);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_realize(IntPtr gtkWidget);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_set_has_window(IntPtr gtkWidget, bool hasWindow);

    [DllImport(LibGtk)]
    internal static extern IntPtr gtk_widget_get_window(IntPtr gtkWidget);

    [DllImport(LibGtk)]
    internal static extern int gtk_widget_get_allocated_width(IntPtr window);

    [DllImport(LibGtk)]
    internal static extern int gtk_widget_get_allocated_height(IntPtr window);

    [DllImport(LibGtk)]
    public static extern bool gtk_events_pending();

    [DllImport(LibGtk)]
    public static extern bool gtk_main_iteration_do(bool blocking);

    [DllImport(LibGtk, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr gtk_offscreen_window_get_pixbuf(IntPtr raw);

    [DllImport(LibGdk, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr gdk_pixbuf_get_from_window(IntPtr gdkWindow, int x, int y, int width, int height);

    [DllImport(LibGdk)]
    internal static extern void gdk_window_set_transient_for(IntPtr window, IntPtr parent);

    [DllImport(LibGdk)]
    internal static extern IntPtr gdk_x11_window_foreign_new_for_display(IntPtr display, IntPtr xid);

    [DllImport(LibGdk)]
    internal static extern IntPtr gdk_display_get_default();
    [DllImport(LibGdk)]
    internal static extern IntPtr gdk_display_get_default_seat(IntPtr display);
    [DllImport(LibGdk)]
    internal static extern IntPtr gdk_seat_get_pointer(IntPtr seat);

    [DllImport(LibGdk)]
    public static extern IntPtr gdk_event_new(GdkEventType type);

    [DllImport(LibGdk)]
    public static extern bool gtk_widget_event(IntPtr widget, IntPtr gdkEvent);

    [DllImport(LibGdk)]
    public static extern void gtk_main_do_event(IntPtr gdkEvent);

    [DllImport(LibGdk)]
    public static extern void gdk_event_free(IntPtr gdkEvent);

    [DllImport(LibGdk)]
    public static extern void gdk_event_put(IntPtr gdkEvent);

    [DllImport(LibGdk)]
    public static extern int gdk_pixbuf_get_width(nint pixbuf);

    [DllImport(LibGdk)]
    public static extern int gdk_pixbuf_get_height(nint pixbuf);

    [DllImport(LibGdk)]
    public static extern int gdk_pixbuf_get_rowstride(nint pixbuf);

    [DllImport(LibGdk)]
    public static extern IntPtr gdk_pixbuf_get_pixels(nint pixbuf);

    [DllImport(LibGdk)]
    public static extern int gdk_pixbuf_get_n_channels(nint pixbuf);

    [DllImport(LibGdk)]
    public static extern IntPtr gdk_pixbuf_add_alpha(nint pixbuf, bool substituteColor, byte r, byte g, byte b);

#if NET7_0_OR_GREATER
    [LibraryImport(LibGdk)]
    public static partial void gdk_window_get_root_coords(IntPtr raw, int x, int y, out int rootX, out int rootY);
#else

    [DllImport(LibGdk)]
    public static extern void gdk_window_get_root_coords(IntPtr raw, int x, int y, out int rootX, out int rootY);
#endif

    [DllImport(LibGtk)]
    internal static extern IntPtr gtk_window_get_title(IntPtr window);

#if NET7_0_OR_GREATER
    [LibraryImport(LibGtk, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void gtk_window_set_title(IntPtr window, string title);
#else
    [DllImport(LibGtk)]
    internal static extern void gtk_window_set_title(IntPtr window, string title);
#endif

    [DllImport(LibGtk)]
    internal static extern void gtk_window_set_default_size(IntPtr window, int width, int height);

    [DllImport(LibGtk)]
    internal static extern void gtk_window_present(IntPtr window);

    [DllImport(LibGtk)]
    internal static extern void gtk_widget_grab_focus(IntPtr widget);

    [DllImport(LibGtk)]
    internal static extern bool gtk_widget_has_focus(IntPtr widget);

#if NET7_0_OR_GREATER
    [LibraryImport(LibGObject, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial ulong g_signal_connect_data(nint instance, string detailed_signal, nint c_handler, nint data, nint destroy_data, GConnectFlags connect_flags);
#else
    [DllImport(LibGObject)]
    internal static extern ulong g_signal_connect_data(nint instance, string detailed_signal, nint c_handler, nint data, nint destroy_data, GConnectFlags connect_flags);
#endif

    [DllImport(LibGObject)]
    internal static extern void g_signal_handler_disconnect(IntPtr instance, ulong handlerId);

    [DllImport(LibWebKit)]
    internal static extern IntPtr webkit_navigation_policy_decision_get_navigation_action(IntPtr decision);

    [DllImport(LibWebKit)]
    internal static extern IntPtr webkit_navigation_action_get_request(IntPtr navigation_action);

    [DllImport(LibWebKit)]
    internal static extern IntPtr webkit_uri_request_get_uri(IntPtr request);

    internal struct GError
    {
        public uint Domain;
        public int Code;
        public nint Message;
    }

    public enum GConnectFlags : int
    {
        AFTER,
        SWAPPED
    }
}
