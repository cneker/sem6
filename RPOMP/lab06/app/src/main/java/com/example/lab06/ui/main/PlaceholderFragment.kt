package com.example.lab06.ui.main

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.lab06.R.drawable.*
import com.example.lab06.databinding.FragmentPlacesBinding

/**
 * A placeholder fragment containing a simple view.
 */
class PlaceholderFragment : Fragment() {

    private lateinit var pageViewModel: PageViewModel
    private var _binding: FragmentPlacesBinding? = null
    private lateinit var imageList: Array<Array<Int>>

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        pageViewModel = ViewModelProvider(this).get(PageViewModel::class.java).apply {
            setIndex(arguments?.getInt(ARG_SECTION_NUMBER) ?: 1)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        _binding = FragmentPlacesBinding.inflate(inflater, container, false)
        val root = binding.root

        val image: ImageView = binding.imageView
        pageViewModel.text.observe(viewLifecycleOwner, Observer {
            when (COUNTRY) {
                "Europe" -> imageList = arrayOf(arrayOf(italy1, italy2), arrayOf(france1, france2),
                    arrayOf(belarus1, belarus2))
                "Africa" -> imageList = arrayOf(arrayOf(cameroun1, cameroun2), arrayOf(egypt1, egypt2),
                    arrayOf(kenya1, kenya2))
                "Asia" -> imageList = arrayOf(arrayOf(japan1, japan2), arrayOf(china1, china2),
                    arrayOf(korea1, korea2))
            }
            when(it) {
                "1" -> {
                    image.setImageResource(imageList[CASE.toInt()][0])
                }
                "2" -> {
                    image.setImageResource(imageList[CASE.toInt()][1])
                }
            }
        })
        return root
    }

    companion object {
        var CASE = "None"
        var COUNTRY = "COUNTRY"
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private const val ARG_SECTION_NUMBER = "section_number"

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        @JvmStatic
        fun newInstance(sectionNumber: Int): PlaceholderFragment {
            return PlaceholderFragment().apply {
                arguments = Bundle().apply {
                    putInt(ARG_SECTION_NUMBER, sectionNumber)
                }
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}