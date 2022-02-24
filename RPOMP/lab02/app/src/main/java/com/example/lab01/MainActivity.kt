package com.example.lab01

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.*

private const val TAG = "MainActivity"

class MainActivity : AppCompatActivity() {

    private lateinit var tvInfo: TextView
    private lateinit var etInput: EditText
    private lateinit var bControl: Button
    private var gameFinished: Boolean = false
    private var guess: Int = 0
    private var inp: Int = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        tvInfo = findViewById(R.id.textView)
        etInput = findViewById(R.id.editTextTextPersonName2)
        bControl = findViewById(R.id.button)
        guess = (Math.random() * 100).toInt()
        Log.i(TAG, guess.toString())

        bControl.setOnClickListener {
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
                    return@setOnClickListener
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
    }
}