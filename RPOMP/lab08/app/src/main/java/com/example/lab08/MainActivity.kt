package com.example.lab08

import android.gesture.Gesture
import android.gesture.GestureLibraries
import android.gesture.GestureLibrary
import android.gesture.GestureOverlayView
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.*
import kotlinx.android.synthetic.main.activity_main.*

private const val TAG = "MainActivity"

class MainActivity : AppCompatActivity(), GestureOverlayView.OnGesturePerformedListener {

    private lateinit var tvInfo: TextView
    private lateinit var etInput: EditText
    private lateinit var bControl: Button
    private var gameFinished: Boolean = false
    private var guess: Int = 0
    private var inp: Int = 0

    private var qLibrary: GestureLibrary? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        tvInfo = findViewById(R.id.textView)
        etInput = findViewById(R.id.editTextTextPersonName2)
        bControl = findViewById(R.id.button)
        guess = (Math.random() * 100).toInt()
        Log.i(TAG, guess.toString())

        bControl.setOnClickListener {
            checkInput()
        }

        gestureSetup()
    }

    private fun gestureSetup() {
        qLibrary = GestureLibraries.fromRawResource(this, R.raw.gestures)

        if(qLibrary?.load() == false)
            finish()
        gOverlay.addOnGesturePerformedListener(this)
    }

    private fun checkInput()
    {
        if (!gameFinished) {
            inp = try {
                Integer.parseInt(etInput.text.toString())
                //etInput.text.toString().toInt()
            } catch (e: Exception) {
                etInput.text.clear()
                -1
            }
            Log.i(TAG, inp.toString())

            if (inp !in 0..100) {
                tvInfo.text = resources.getString(R.string.error)
                etInput.text.clear()
                return
            }
            if (inp > guess) {
                tvInfo.text = resources.getString(R.string.ahead)
            }
            if (inp < guess) {
                tvInfo.text = resources.getString(R.string.behind)
            }
            if (inp == guess) {
                tvInfo.text = resources.getString(R.string.hit)
                bControl.text = resources.getString(R.string.play_more)
                gameFinished = true
            }
        }
        else {
            guess = (Math.random() * 100).toInt()
            bControl.text = resources.getString(R.string.input_value)
            tvInfo.text = resources.getString(R.string.try_to_guess)
            gameFinished = false
        }
        etInput.text.clear()
    }

    override fun onGesturePerformed(overlay: GestureOverlayView?, gesture: Gesture?) {
        val predictions = qLibrary?.recognize(gesture)
        predictions?.let {
            if(it.size > 0 && it[0].score > 1.0) {
                val action = it[0].name

                val prevent_text = editTextTextPersonName2.text.toString()
                when(action) {
                    "zero" -> editTextTextPersonName2.setText(prevent_text + "0")
                    "one" -> editTextTextPersonName2.setText(prevent_text + "1")
                    "two" -> editTextTextPersonName2.setText(prevent_text + "2")
                    "three" -> editTextTextPersonName2.setText(prevent_text + "3")
                    "four" -> editTextTextPersonName2.setText(prevent_text + "4")
                    "five" -> editTextTextPersonName2.setText(prevent_text + "5")
                    "six" -> editTextTextPersonName2.setText(prevent_text + "6")
                    "seven" -> editTextTextPersonName2.setText(prevent_text + "7")
                    "eight" -> editTextTextPersonName2.setText(prevent_text + "8")
                    "nine" -> editTextTextPersonName2.setText(prevent_text + "9")
                    "clear" -> checkInput()
                }
                Toast.makeText(this, action, Toast.LENGTH_SHORT).show()
            }
        }
    }
}