/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        colors: {
            'white': '#FFFFFF',
            'grey-50': '#eff1f3',
            'grey-100': '#edeef1',
            'grey-200': 'rgb(229 231 235)',
            'grey-300': 'rgb(209 213 219)',
            'blue-50': '#f0f9ff',
            'blue-100': '#e0f2fe',
            'blue-200': '#bfdbfe',
            'blue-300': '#7dd3fc',
            'blue-400': '#38bdf8',
            'blue-500': '#0ea5e9',
            'blue-600': '#0284c7',
            'blue-700': '#0369a1'
        },
     screens: {
          'width-xxsm': { 'raw': '(max-width: 300px)' },
          'width-xsm': { 'raw': '(mim-width: 480px)' },
          'width-sm': { 'raw': '(min-width: 560px)' },
          'width-md': { 'raw': '(min-width: 768px)' },
          'width-lg': { 'raw': '(min-width: 1024px)' },
          'width-xl': { 'raw': '(min-width: 1280px)' },
          'width-2xl': { 'raw': '(min-width: 1536px)' },
     }
  },
  plugins: [],
}

