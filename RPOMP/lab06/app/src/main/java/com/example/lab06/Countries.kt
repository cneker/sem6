package com.example.lab06

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.ListView
import com.example.lab06.ui.main.PlaceholderFragment

class Countries : AppCompatActivity() {

    companion object {
        var CONTINENT = "1"
    }

    private lateinit var arrayAdapter : ArrayAdapter<String>
    //private var continent = "None"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_countries)

        val listview = findViewById<ListView>(R.id.listview)
        val names1 = arrayOf("Italy", "France", "Belarus")
        val names2 = arrayOf("Cameroun", "Egypt", "Kenya")
        val names3 = arrayOf("Japan", "China", "Korea")
        CONTINENT = intent.getStringExtra(CONTINENT)!!
        when(CONTINENT) {

            "Europe" -> {
                arrayAdapter = ArrayAdapter(
                    this, android.R.layout.simple_list_item_1, names1
                )
            }
            "Africa" -> {
                arrayAdapter = ArrayAdapter(
                    this, android.R.layout.simple_list_item_1, names2
                )
            }
            "Asia" -> {
                arrayAdapter = ArrayAdapter(
                    this, android.R.layout.simple_list_item_1, names3
                )
            }
        }

        listview.adapter = arrayAdapter
        listview.setOnItemClickListener { adapterView, view, i, l ->

            val inet = Intent(this, Places::class.java)
            when(i) {
                0 -> {
                    inet.putExtra(PlaceholderFragment.CASE, "0")
                }
                1 -> {
                    inet.putExtra(PlaceholderFragment.CASE, "1")
                }
                2 -> {
                    inet.putExtra(PlaceholderFragment.CASE, "2")
                }
            }
            inet.putExtra(PlaceholderFragment.COUNTRY, CONTINENT)
            startActivity(inet)
        }
    }
}