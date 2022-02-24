package com.example.lab071

import android.app.Activity
import android.view.GestureDetector
import android.widget.TextView
import androidx.core.view.GestureDetectorCompat
import android.os.Bundle
import android.view.MotionEvent
import android.view.View

internal class MainActivity : Activity(), GestureDetector.OnGestureListener,
    GestureDetector.OnDoubleTapListener {
    var tvOutput: TextView? = null
    var mDetector: GestureDetectorCompat? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        tvOutput = findViewById<View>(R.id.textView) as TextView
        mDetector = GestureDetectorCompat(this, this)
        mDetector!!.setOnDoubleTapListener(this)
    }

    override fun onTouchEvent(event: MotionEvent): Boolean {
        mDetector!!.onTouchEvent(event)
        // Be sure to call the superclass implementation
        return super.onTouchEvent(event)
    }

    override fun onDown(event: MotionEvent): Boolean {
        tvOutput!!.text = "onDown: $event"
        return false
    }

    override fun onFling(
        event1: MotionEvent, event2: MotionEvent,
        velocityX: Float, velocityY: Float
    ): Boolean {
        tvOutput!!.text = "onFling: $event1$event2"
        return true
    }

    override fun onLongPress(event: MotionEvent) {
        tvOutput!!.text = "onLongPress: $event"
    }

    override fun onScroll(
        e1: MotionEvent, e2: MotionEvent, distanceX: Float,
        distanceY: Float
    ): Boolean {
        tvOutput!!.text = "onScroll: $e1$e2"
        return true
    }

    override fun onShowPress(event: MotionEvent) {
        tvOutput!!.text = "onShowPress: $event"
    }

    override fun onSingleTapUp(event: MotionEvent): Boolean {
        tvOutput!!.text = "onSingleTapUp: $event"
        return true
    }

    override fun onDoubleTap(event: MotionEvent): Boolean {
        tvOutput!!.text = "onDoubleTap: $event"
        return true
    }

    override fun onDoubleTapEvent(event: MotionEvent): Boolean {
        tvOutput!!.text = "onDoubleTapEvent: $event"
        return true
    }

    override fun onSingleTapConfirmed(event: MotionEvent): Boolean {
        tvOutput!!.text = "onSingleTapConfirmed: $event"
        return true
    }
}