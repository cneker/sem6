package com.example.memorygame

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ImageButton
import android.widget.Toast
import kotlinx.android.synthetic.main.activity_main.*
import com.example.memorygame.R.drawable.*

private const val TAG = "MainActivity"

class MainActivity : AppCompatActivity() {

    private lateinit var buttons: List<ImageButton>
    private lateinit var cards: List<MemoryCard>
    private var indexOfSingleSelectedCard: Int? = null
    private var matchResult: Boolean = true

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val images = mutableListOf(bear, bee, bird, bug, butterfly, cat, coala, dog)
        images.addAll(images)
        images.shuffle()

        buttons = listOf(imageButton1, imageButton2, imageButton3, imageButton4, imageButton5, imageButton6,
            imageButton7, imageButton8, imageButton10, imageButton11, imageButton12, imageButton13,
            imageButton14, imageButton15, imageButton16, imageButton18)

        cards = buttons.indices.map { index ->
            MemoryCard(images[index])
        }

        button.setOnClickListener {
            indexOfSingleSelectedCard = null
            cards.forEachIndexed { index, memoryCard ->
                val button = buttons[index]
                button.alpha = 1.0f
                memoryCard.isFaceUp = false
                memoryCard.isMatched = false
                memoryCard.isSelected = false
                button.setImageResource(ic_creative_tail_animal_camel)
            }
            images.shuffle()
            cards = buttons.indices.map { index ->
                MemoryCard(images[index])
            }
            matchResult = true
        }

        buttons.forEachIndexed { index, imageButton ->
            imageButton.setOnClickListener {
                Log.i(TAG, "button clicked")

                updateModels(index, imageButton)

                updateViews()

                updateView()
            }
        }
    }

    private fun updateView() {
        if (!matchResult) {
            Thread {
                Thread.sleep(300)
                runOnUiThread {
                    cards.forEachIndexed { index, memoryCard ->
                        val button = buttons[index]
                        if (memoryCard.isSelected && memoryCard.isFaceUp) {
                            button.setImageResource(ic_creative_tail_animal_camel)
                            memoryCard.isSelected = false
                            memoryCard.isFaceUp = false
                        }
                    }
                }
            }.start()

        }
        matchResult = true
    }

    private fun updateViews() {
        cards.forEachIndexed { index, memoryCard ->
            val button = buttons[index]
            if (memoryCard.isMatched)
                button.alpha = 0.1f
            button.setImageResource(if (memoryCard.isFaceUp) memoryCard.identifier else ic_creative_tail_animal_camel)
        }

    }

    private fun updateModels(index: Int, imageButton: ImageButton) {
        val card = cards[index]
        //Error checking
        if (card.isFaceUp){
            return
        }

        if(indexOfSingleSelectedCard == null) {
            //0 or 2 selected cards
            restoreCards()
            indexOfSingleSelectedCard = index
            card.isSelected = true
        }
        else {
            if (indexOfSingleSelectedCard != null) {
                //1 card
                matchResult = checkForMatch(indexOfSingleSelectedCard!!, index)
                indexOfSingleSelectedCard = null
                card.isSelected = true
            }
        }

        card.isFaceUp = !card.isFaceUp
    }

    private fun restoreCards() {
        for (card in cards) {
            if (!card.isMatched) {
                card.isFaceUp = false
            }
            //card.isSelected = false
        }
    }

    private fun checkForMatch(index1: Int, index2: Int) : Boolean {
        if(cards[index1].identifier == cards[index2].identifier) {
            Toast.makeText(this, "Match found", Toast.LENGTH_SHORT).show()
            cards[index1].isMatched = true
            cards[index2].isMatched = true
            return true
        }
        return false
    }
}