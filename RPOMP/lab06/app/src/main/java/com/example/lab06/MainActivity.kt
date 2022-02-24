package com.example.lab06

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import androidx.appcompat.app.AlertDialog
import com.example.lab06.R.string.*

class MainActivity : AppCompatActivity() {

    private lateinit var continentButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        continentButton = findViewById(R.id.button)
        continentButton.setOnClickListener {

            val list: Array<String> = arrayOf(getText(africa).toString(), getText(europe).toString(),
                getText(asia).toString())

            var builder: AlertDialog.Builder = AlertDialog.Builder(this)
            builder.setTitle(R.string.message)
            builder.setItems(list) { _, which ->
                val selected = list[which]
                val inet = Intent(this, Countries::class.java)
                when (selected){
                    "Европа" -> {
                        inet.putExtra(Countries.CONTINENT, "Europe")
                    }
                    "Африка" -> {
                        inet.putExtra(Countries.CONTINENT, "Africa")
                    }
                    "Азия" -> {
                        inet.putExtra(Countries.CONTINENT, "Asia")
                    }
                }
                startActivity(inet)
            }
            var dialog = builder.create()
            dialog.show()
        }
    }
}