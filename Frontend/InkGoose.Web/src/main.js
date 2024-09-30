import { createApp } from 'vue'
import App from './App.vue'
import './style.css'

const app = createApp(App)
app.config.globalProperties.apiHost = "https://localhost:7264/api/Notes"
app.mount('#app')

