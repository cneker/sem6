package com.example.lab05kt

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.ListAdapter
import android.widget.ListView
import android.widget.ProgressBar
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import org.json.JSONObject
import org.json.JSONArray
import org.json.JSONException
import com.android.volley.toolbox.Volley
import java.io.UnsupportedEncodingException
import java.util.ArrayList

class MainActivity : AppCompatActivity() {
    var listView: ListView? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        listView = findViewById<View>(R.id.listView) as ListView
        loadJSONFromURL(JSON_URL)
    }

    private fun loadJSONFromURL(url: String) {
        val progressBar = findViewById<View>(R.id.progressBar) as ProgressBar
        progressBar.visibility = ListView.VISIBLE
        val stringRequest = StringRequest(
            Request.Method.GET, url,
            { response ->
                progressBar.visibility = View.INVISIBLE
                try {
                    val `object` = JSONObject(EncodingToUTF8(response))
                    //getJSONArray - извлекает массив
                    val jsonArray = `object`.getJSONArray("users")
                    //по ключам получаем значения
                    val listItems = getArrayListFromJSONArray(jsonArray)
                    //передаем список в адаптер, а он уже занимается его выводом
                    val adapter: ListAdapter = ListViewAdapter(
                        applicationContext,
                        R.layout.row,
                        R.id.textViewName,
                        listItems
                    )
                    listView!!.adapter = adapter
                } catch (e: JSONException) {
                    e.printStackTrace()
                }
            }
        ) { error -> Log.d("error", error.toString()) }
        val requestQueue = Volley.newRequestQueue(this)
        requestQueue.add(stringRequest)
    }

    //по ключам получаем значения
    private fun getArrayListFromJSONArray(jsonArray: JSONArray?): ArrayList<JSONObject> {
        val aList = ArrayList<JSONObject>()
        try {
            if (jsonArray != null) {
                for (i in 0 until jsonArray.length()) {
                    aList.add(jsonArray.getJSONObject(i))
                }
            }
        } catch (js: JSONException) {
            js.printStackTrace()
        }
        return aList
    }

    companion object {
        private const val JSON_URL = "http://m1.maxfad.ru/api/users.json" // UTF-8

        //чтобы имена не выводились набором загогулек
        fun EncodingToUTF8(response: String): String? {
            var response = response
            response = try {
                val code = response.toByteArray(charset("ISO-8859-1"))
                String(code, charset("UTF-8"))
            } catch (e: UnsupportedEncodingException) {
                e.printStackTrace()
                return null
            }
            return response
        }
    }
}