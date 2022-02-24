package com.example.lab071

import android.app.Activity
import androidx.core.view.GestureDetectorCompat
import android.widget.TextView
import android.os.Bundle
import android.view.MotionEvent
import android.view.GestureDetector.SimpleOnGestureListener
import android.view.View

internal class AdditionalActivity : Activity() {
    private var mDetector: GestureDetectorCompat? = null
    private var tvOut: TextView? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        mDetector = GestureDetectorCompat(this, MyGestListener())
        tvOut = findViewById<View>(R.id.textView) as TextView
    }

    override fun onTouchEvent(event: MotionEvent): Boolean {
        mDetector!!.onTouchEvent(event)
        return super.onTouchEvent(event)
    }

    internal inner class MyGestListener : SimpleOnGestureListener() {
        override fun onFling(
            event1: MotionEvent, event2: MotionEvent,
            velocityX: Float, velocityY: Float
        ): Boolean {
            tvOut!!.text = "onFling: $event1$event2"
            return true
        }
    }
}