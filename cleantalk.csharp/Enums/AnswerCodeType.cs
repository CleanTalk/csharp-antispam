// ReSharper disable InconsistentNaming

using System.ComponentModel.DataAnnotations;

namespace cleantalk.csharp.Enums
{
    public enum AnswerCodeType
    {
        [Display(Name = "Allowed.")]
        ALLOWED,

        [Display(Name = "Private list allow.")]
        ALLOWED_PRIV_LIST,

        [Display(Name = "Profile allowed.")]
        ALLOWED_PROFILE,

        [Display(Name = "User allowed.")]
        ALLOWED_USER,

        [Display(Name = "Check plugin setup.")]
        BAD_INSTALL,

        [Display(Name = "Contains bad language.")]
        BAD_LANGUAGE,

        [Display(Name = "HTTP links blacklisted.")]
        BL_DOMAIN,

        [Display(Name = "Sender blacklisted.")]
        BL,

        [Display(Name = "Trackback, Pingback comment's type need manual moderation.")]
        COMMENT_TYPE_UNKNOWN,

        [Display(Name = "Contains links.")]
        CONTACTS,

        [Display(Name = "Contains contacts.")]
        CONTACTS_DATA,

        [Display(Name = "Forbidden.")]
        DENIED,

        [Display(Name = "Please submit form again.")]
        DENIED_GREY_LIST,

        [Display(Name = "Private list deniy.")]
        DENIED_PRIV_LIST,

        [Display(Name = "Profile forbidden.")]
        DENIED_PROFILE,

        [Display(Name = "User forbidden.")]
        DENIED_USER,

        [Display(Name = "Site visitor IP is rqual to server site IP.")]
        ERR_CLIENT_IP_EQ_SERVER_IP,

        [Display(Name = "Submitted too quickly.")]
        FAST_SUBMIT,

        [Display(Name = "Forbidden.")]
        FORBIDDEN,

        [Display(Name = "Please enable JavaScript.")]
        JS_DISABLED,

        [Display(Name = "Antispam disabled. Check access key.")]
        KEY_NOT_FOUND,

        [Display(Name = "Need manually approve.")]
        MANUAL,

        [Display(Name = "Massive posting.")]
        MULT_MESSAGE,

        [Display(Name = "Multiple comments submit.")]
        MULT_SUBMIT,

        [Display(Name = "Without dictionary words.")]
        NO_NORM_WORDS,

        [Display(Name = "Offtopic.")]
        OFFTOP,

        [Display(Name = "Service disabled. Check account status.")]
        SERVICE_DISABLED,

        [Display(Name = "Service freezed. Please extend limit.")]
        SERVICE_FREEZED,

        [Display(Name = "Contains stop words.")]
        STOP_LIST,

        [Display(Name = "Trial period expired.")]
        TRIAL_EXPIRED,

        [Display(Name = "Spam sender name.")]
        USERNAME_SPAM,

        [Display(Name = "Wrong timezone.")]
        WRONG_TZ
    }
}