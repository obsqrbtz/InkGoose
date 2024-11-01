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
                },
                goosedark: {
                    "primary": "#ff00c5",
                    "primary-content": "#16000e",
                    "secondary": "#862700",
                    "secondary-content": "#ead3cc",
                    "accent": "#719e00",
                    "accent-content": "#040900",
                    "neutral": "#060606",
                    "neutral-content": "#c5c5c5",
                    "base-100": "#212635",
                    "base-200": "#1b202d",
                    "base-300": "#161925",
                    "base-content": "#cdcfd3",
                    "info": "#00e1ff",
                    "info-content": "#001216",
                    "success": "#00e4af",
                    "success-content": "#00120b",
                    "warning": "#c33a00",
                    "warning-content": "#f8d9d0",
                    "error": "#ff4f84",
                    "error-content": "#160206",
                },
            },
        ],
      },
}