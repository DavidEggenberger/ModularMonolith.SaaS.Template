/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
      extend: {
          gridTemplateRows: {
              '40px-12rows': '40px repeat(12, minmax(0, 1fr)) ',
              '50px-12rows': '50px repeat(12, minmax(0, 1fr)) ',
              '80px-12rows': '80px repeat(12, minmax(0, 1fr)) ',
          },
      },
      screens: {
          'height-xsm': { 'raw': '(min-height: 400px)' },
          'height-sm': { 'raw': '(min-height: 520px)' },
          'height-md': { 'raw': '(min-height: 640px)' },
          'height-lg': { 'raw': '(min-height: 800px)' },
          'height-xl': { 'raw': '(min-height: 1000px)' },
          'height-2xl': { 'raw': '(min-height: 1300px)' },

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

