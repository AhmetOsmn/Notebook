export default function reducer(state, action) {
    switch (action.type) {
        case 'TOGGLE_THEME':
            const theme = state.theme === 'light' ? 'dark' : 'light';
            localStorage.setItem('theme', theme)
            return {
                ...state,
                theme
            }
        case 'TOGGLE_LANGUAGE':
            const language = state.language === 'en' ? 'tr' : 'en';
            localStorage.setItem('language', language)
            return {
                ...state,
                language
            }
    }
}