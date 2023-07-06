/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
      extend: {
          gridTemplateRows: {
              '60px-12rows': '60px repeat(12, minmax(0, 1fr)) ',
          },
      },
      screens: {
          'height-xsm': { 'raw': '(min-height: 400px)' },
          'height-sm': { 'raw': '(min-height: 520px)' },
          'height-md': { 'raw': '(min-height: 640px)' },
          'height-lg': { 'raw': '(min-height: 800px)' },
          'height-xl': { 'raw': '(min-height: 1000px)' },
          'height-2xl': { 'raw': '(min-height: 1300px)' },

          'width-sm': { 'raw': '(min-width: 640px)' },
          'width-md': { 'raw': '(min-width: 768px)' },
          'width-lg': { 'raw': '(min-width: 1024px)' },
          'width-xl': { 'raw': '(min-width: 1280px)' },
          'width-2xl': { 'raw': '(min-width: 1536px)' },
      }
  },
  plugins: [],
}

