package com.google.android.games;

import android.app.Activity;
import android.content.Context;
import android.os.Build;
import android.util.Log;
import android.view.Display;
import android.view.Surface;
import android.view.SurfaceView;
import android.view.WindowManager;

import com.unity3d.player.R;

public class DisplayUtil {

    private Display _display;
    private Surface _targetSurface;

    public void init(Context context) {
        Activity activity = (Activity) context;

        //Get the Display object
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.R) {
            _display = activity.getDisplay();
        } else {
            _display = ((WindowManager) activity.getSystemService(Context.WINDOW_SERVICE)).getDefaultDisplay();
        }

        //Cache the Unity's Surface View
        SurfaceView surfaceView = activity.findViewById(R.id.unitySurfaceView);
        _targetSurface = surfaceView.getHolder().getSurface();
    }

    public void setDisplayRefreshRate(int refreshRate) {
        Log.e("OTTO", "Try Set Display Refresh Rate to " + refreshRate);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.R) {
            _targetSurface.setFrameRate(refreshRate, Surface.FRAME_RATE_COMPATIBILITY_DEFAULT);
            Log.e("OTTO", "SetDisplayRefreshRate compatible " + refreshRate);
        }
    }

    public float getDisplayRefreshRate() {
        float refreshRate = _display.getRefreshRate();
        Log.e("OTTO", "GetDisplayRefreshRate " + refreshRate);
        return refreshRate;
    }
}
