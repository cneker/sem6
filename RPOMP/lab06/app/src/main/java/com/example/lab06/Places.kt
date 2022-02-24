package com.example.lab06

import android.os.Bundle
import com.google.android.material.tabs.TabLayout
import androidx.viewpager.widget.ViewPager
import androidx.appcompat.app.AppCompatActivity
import com.example.lab06.ui.main.SectionsPagerAdapter
import com.example.lab06.databinding.ActivityPlacesBinding
import com.example.lab06.ui.main.PlaceholderFragment

class Places : AppCompatActivity() {

    private lateinit var binding: ActivityPlacesBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityPlacesBinding.inflate(layoutInflater)
        setContentView(binding.root)
        PlaceholderFragment.CASE = intent.getStringExtra(PlaceholderFragment.CASE)!!
        PlaceholderFragment.COUNTRY = intent.getStringExtra(PlaceholderFragment.COUNTRY)!!
        val sectionsPagerAdapter = SectionsPagerAdapter(this, supportFragmentManager)

        val viewPager: ViewPager = binding.viewPager
        viewPager.adapter = sectionsPagerAdapter
        val tabs: TabLayout = binding.tabs
        tabs.setupWithViewPager(viewPager)
    }
}