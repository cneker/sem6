package com.example.lab05kt

import android.content.Context
import org.json.JSONObject
import android.widget.ArrayAdapter
import android.view.ViewGroup
import android.view.LayoutInflater
import android.view.View
import android.widget.TextView
import org.json.JSONException
import java.util.ArrayList

class ListViewAdapter     //конструктор класса
    (context: Context, var listLayout: Int, field: Int, var usersList: ArrayList<JSONObject>) :
    ArrayAdapter<JSONObject?>(
        context, listLayout, field, usersList as List<JSONObject?>
    ) {
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        //Создаем новый экземпляр LayoutInflater, связанный с определенным контекстом
        val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
        val listViewItem = inflater.inflate(listLayout, null)
        val name = listViewItem.findViewById<TextView>(R.id.textViewName)
        val email = listViewItem.findViewById<TextView>(R.id.textViewEmail)
        try {
            name.text = usersList[position].getString("name")
            email.text = usersList[position].getString("email")
        } catch (je: JSONException) {
            je.printStackTrace()
        }
        return listViewItem
    }
}