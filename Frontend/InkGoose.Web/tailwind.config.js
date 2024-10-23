/** @type {import('tailwindcss').Config} */
export default {
    content: [
        "./index.html",
        "./src/**/*.{vue,js,ts,jsx,tsx}",
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography'),
        require('daisyui'),
    ],
    daisyui: {
        themes: [
            {
                goosesark: {
                    "primary": "#57f8a7",
                    "primary-content": "#03150a",
                    "secondary": "#482cf7",
                    "secondary-content": "#d3dbff",
                    "accent": "#f5a12b",
                    "accent-content": "#150901",
                    "neutral": "#262931",
                    "neutral-content": "#cfd0d2",
                    "base-100": "#191d2b",
                    "base-200": "#141824",
                    "base-300": "#0f131d",
                    "base-content": "#cbccd0",
                    "info": "#2563EB",
                    "info-content": "#d2e2ff",
                    "success": "#16A34A",
                    "success-content": "#000a02",
                    "warning": "#D97706",
                    "warning-content": "#110500",
                    "error": "#DC2626",
                    "error-content": "#ffd9d4"
                },
                gooselight: {
                    "primary": "#a529fa",
                    "primary-content": "#ecdbff",
                    "secondary": "#f9d75b",
                    "secondary-content": "#151103",
                    "accent": "#51f56a",
                    "accent-content": "#021504",
                    "neutral": "#262931",
                    "neutral-content": "#cfd0d2",
                    "base-100": "#f5eceb",
                    "base-200": "#d5cdcc",
                    "base-300": "#b6afae",
                    "base-content": "#151313",
                    "info": "#2563EB",
                    "info-content": "#d2e2ff",
                    "success": "#16A34A",
                    "success-content": "#000a02",
                    "warning": "#D97706",
                    "warning-content": "#110500",
                    "error": "#DC2626",
                    "error-content": "#ffd9d4"
                }
            },
        ],
      },
}