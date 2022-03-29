package com.example.lab10

import android.Manifest
import android.app.Activity
import android.content.Context
import android.content.pm.PackageManager
import android.location.Location
import android.location.LocationListener
import android.location.LocationManager
import android.os.Bundle
import android.view.View
import android.widget.TextView
import androidx.core.app.ActivityCompat

class MainActivity : Activity() {
    var tvOut: TextView? = null
    var tvLon: TextView? = null
    var tvLat: TextView? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        tvOut = findViewById<View>(R.id.textView1) as TextView
        tvLon = findViewById<View>(R.id.longitude) as TextView
        tvLat = findViewById<View>(R.id.latitude) as TextView

        //Получаем сервис
        val mlocManager = getSystemService(Context.LOCATION_SERVICE) as LocationManager
        val mlocListener: LocationListener = object : LocationListener {
            override fun onLocationChanged(location: Location) {

                //Called when a new location is found by the network location provider.
                tvLat!!.append("\n" + location.latitude)
                tvLon!!.append("\n" + location.longitude)
            }

            override fun onStatusChanged(provider: String, status: Int, extras: Bundle) {}
            override fun onProviderEnabled(provider: String) {}
            override fun onProviderDisabled(provider: String) {}
        }

        //Подписываемся на изменения в показаниях датчика
        if (ActivityCompat.checkSelfPermission(
                this,
                Manifest.permission.ACCESS_FINE_LOCATION
            ) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(
                this,
                Manifest.permission.ACCESS_COARSE_LOCATION
            ) != PackageManager.PERMISSION_GRANTED
        ) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return
        }
        mlocManager.requestLocationUpdates(
            LocationManager.GPS_PROVIDER, 0, 0f,
            mlocListener
        )

        //Если gps включен, то ... , иначе вывести "GPS is not turned on..."
        if (mlocManager.isProviderEnabled(LocationManager.GPS_PROVIDER)) {
            tvOut!!.text = "GPS is turned on..."
        } else {
            tvOut!!.text = "GPS is not turned on..."
        }
    }
}