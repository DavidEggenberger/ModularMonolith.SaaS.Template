/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
      extend: {
          gridTemplateRows: {
              'main': '80px repeat(10, minmax(0, 1fr)) 60px',
              'footer': '200px minmax(900px, 1fr) 100px',
          }
      }
  },
  plugins: [],
}

