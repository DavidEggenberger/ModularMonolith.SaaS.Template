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
            'primary-50': '#fdf4ff',
            'primary-100': '#fae8ff',
            'primary-200': '#f5d0fe',
            'primary-300': '#f0abfc',
            'primary-400': '#e879f9',
            'primary-500': '#d946ef',
            'primary-600': '#c026d3',
            'primary-700': '#a21caf',
            'primary-800': '#86198f',
            'primary-900': '#4c1d95'
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

