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
            'primary-50': '#f0f9ff',
            'primary-100': '#e0f2fe',
            'primary-200': '#bae6fd',
            'primary-300': '#7dd3fc',
            'primary-400': '#38bdf8',
            'primary-500': '#0ea5e9',
            'primary-600': '#0284c7',
            'primary-700': '#0369a1',
            'primary-800': '#075985',
            'primary-900': '#0c4a6e'
        },
         screens: {
             'xs': { 'raw': '(min-width: 0px)' },
             'sm': { 'raw': '(min-width: 480px)' },
             'md': { 'raw': '(min-width: 768px)' },
             'lg': { 'raw': '(min-width: 1024px)' },
             'xl': { 'raw': '(min-width: 1280px)' }
         }
  },
  plugins: [],
}

